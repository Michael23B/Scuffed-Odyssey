using System;
using Facepunch.Steamworks;
using UnityEngine;

public static class NetworkEvents
{
    public static void SendPlayerPosition(float x, float y)
    {
        string[] args = { Math.Round(x, 2).ToString(), Math.Round(y, 2).ToString() };
        byte[] data = PacketHandler.SerializePacket(Constants.PacketType.PlayerPosition, args);
        SendToLobby(data);
    }

    public static void SendPlayerSpawned()
    {
        byte[] data = PacketHandler.SerializePacket(Constants.PacketType.PlayerSpawned);
        SendToLobby(data);
    }

    // Send data packet to each other player in the lobby
    private static void SendToLobby(byte[] data)
    {
        foreach (var memberId in Client.Instance.Lobby.GetMemberIDs())
        {
            if (memberId != Client.Instance.SteamId)
            {
                Client.Instance.Networking.SendP2PPacket(memberId, data, data.Length);
            }
        }
    }

    /**
     * Callback methods.
     * Can be overridden i.e. NetworkEvents.OnPlayerPosition = (args) => {...}.
     */
    public static Action<PlayerPositionEventArgs> OnPlayerPosition = (args) =>
    {
        var senderPlayer = GameData.Instance.ClientPlayers.Find(player => player.PlayerId == args.SteamId);

        if (senderPlayer)
        {
            senderPlayer.RigidBody.MovePosition(new Vector2(args.X, args.Y));
        }
    };

    public static Action<PlayerSpawnedEventArgs> OnPlayerSpawn = (args) =>
    {
        GameData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, args.SteamId));
    }; 
    
}
