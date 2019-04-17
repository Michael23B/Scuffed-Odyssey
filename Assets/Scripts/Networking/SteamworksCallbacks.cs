using System;
using System.Text;
using Facepunch.Steamworks;
using UnityEngine;

public static class SteamworksCallbacks
{
    public static void Initialize()
    {
        Client.Instance.Lobby.OnLobbyCreated = OnLobbyCreated;
        Client.Instance.Lobby.OnLobbyJoined = OnLobbyJoined;
        Client.Instance.LobbyList.OnLobbiesUpdated = OnLobbiesUpdated;
        Client.Instance.Networking.OnP2PData = OnP2PData;
    }

    private static readonly Action<bool> OnLobbyCreated = (success) =>
    {
        Debug.Log($"Lobby Created? {success}");
        if (success)
        {
            // Create local player
            if (!NetworkData.Instance.LocalPlayer)
            {
                NetworkData.Instance.LocalPlayer = NetworkPlayer.CreateNetworkPlayer(true, Client.Instance.SteamId);
            }
        }
    };

    private static readonly Action<bool> OnLobbyJoined = (success) =>
    {
        Debug.Log($"Lobby Joined? {success}");
        if (success)
        {
            var data = Encoding.UTF8.GetBytes("spawn"); // Spawn packet

            if (!NetworkData.Instance.LocalPlayer)
            {
                NetworkData.Instance.LocalPlayer = NetworkPlayer.CreateNetworkPlayer(true, Client.Instance.SteamId);
            }

            // Create a player for each other player in the lobby
            foreach (var memberId in Client.Instance.Lobby.GetMemberIDs())
            {
                if (memberId != Client.Instance.SteamId)
                {
                    NetworkData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, memberId));

                    // Send this player a packet to spawn a player for you on their screen
                    Client.Instance.Networking.SendP2PPacket(memberId, data, data.Length);
                }
            }
        }
    };

    private static readonly Action OnLobbiesUpdated = () =>
    {
        if (Client.Instance.LobbyList.Finished)
        {
            Debug.Log($"Found {Client.Instance.LobbyList.Lobbies.Count} lobbies");

            foreach (LobbyList.Lobby lobby in Client.Instance.LobbyList.Lobbies)
            {
                Debug.Log($"Found Lobby: {lobby.Name}");
            }
        }
    };

    private static readonly Networking.OnRecievedP2PData OnP2PData = (steamid, bytes, length, channel) =>
    {
        // TODO this should defer to another class that handles receiving/sending data
        var str = Encoding.UTF8.GetString(bytes, 0, length);

        // Spawn packet, create a NetworkPlayer for the new player
        if (str == "spawn")
        {
            NetworkData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, steamid));
            return;
        }

        var senderPlayer = NetworkData.Instance.ClientPlayers.Find(player => player.PlayerId == steamid);

        if (senderPlayer)
        {
            string[] data = str.Split('?');
            float senderX = float.Parse(data[0]);
            float senderY = float.Parse(data[1]);

            senderPlayer.transform.position = new Vector3(senderX, senderY);
        }
    };
}
