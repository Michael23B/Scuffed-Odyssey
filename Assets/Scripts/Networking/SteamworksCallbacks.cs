using System;
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
            if (!GameData.Instance.LocalPlayer)
            {
                GameData.Instance.LocalPlayer = NetworkPlayer.CreateNetworkPlayer(true, Client.Instance.SteamId);
            }
        }
    };

    private static readonly Action<bool> OnLobbyJoined = (success) =>
    {
        Debug.Log($"Lobby Joined? {success}");
        if (success)
        {
            Debug.Log($"Joined {Client.Instance.Lobby.Name}");
            // If we don't have a local player, create one
            if (!GameData.Instance.LocalPlayer)
            {
                GameData.Instance.LocalPlayer = NetworkPlayer.CreateNetworkPlayer(true, Client.Instance.SteamId);
            }

            // Spawn a network player for each client in the lobby
            foreach (var memberId in Client.Instance.Lobby.GetMemberIDs())
            {
                if (memberId != Client.Instance.SteamId)
                {
                    GameData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, memberId));
                }
            }

            // Send a message to spawn your player locally for other clients
            NetworkEvents.SendPlayerSpawned();
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
        PacketHandler.HandlePacket(steamid, bytes, length);
    };
}
