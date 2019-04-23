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

    public static void SendPlayerFired(Vector2 origin, Vector2 target, bool isSpecial)
    {
        string[] args = { origin.x.ToString(), origin.y.ToString(), target.x.ToString(), target.y.ToString(), isSpecial.ToString() };
        byte[] data = PacketHandler.SerializePacket(Constants.PacketType.PlayerFired, args);
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
            senderPlayer.Player.LerpMovement.StartMoving(new Vector2(args.X, args.Y));
//            senderPlayer.RigidBody.MovePosition(new Vector2(args.X, args.Y));
        }
    };

    public static Action<PlayerSpawnedEventArgs> OnPlayerSpawn = (args) =>
    {
        GameData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, args.SteamId));
    };

    public static Action<PlayerFiredEventArgs> OnPlayerFire = (args) =>
    {
        var senderPlayer = GameData.Instance.ClientPlayers.Find(player => player.PlayerId == args.SteamId);

        if (senderPlayer)
        {
            senderPlayer.Player.FireGun(new Vector2(args.X, args.Y), new Vector2(args.MouseX, args.MouseY), args.IsSpecial);
        }
    };
}
