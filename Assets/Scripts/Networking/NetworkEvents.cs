using System;
using Facepunch.Steamworks;
using FlatBuffers;
using UnityEngine;

public static class NetworkEvents
{
    static readonly FlatBufferBuilder Fbb = new FlatBufferBuilder(1);

    public static void SendPlayerPosition(float x, float y)
    {
        // Write data
        Fbb.Clear();
        var unit = UnitPosition.CreateUnitPosition(Fbb, x, y);
        Fbb.Finish(unit.Value);
        // Add packet type byte to start of data
        byte[] data = Fbb.SizedByteArray().PrependByteArray((byte)Constants.PacketType.PlayerPosition);

        SendToLobby(data);
    }

    public static void SendPlayerSpawned()
    {
        byte[] data = {(byte) Constants.PacketType.PlayerSpawned};
        SendToLobby(data);
    }

    public static void SendPlayerFired(Vector2 origin, Vector2 target, bool isSpecial)
    {
        // Write data
        Fbb.Clear();
        var unit = UnitFire.CreateUnitFire(Fbb, origin.x, origin.y, target.x, target.y, isSpecial);
        Fbb.Finish(unit.Value);
        // Add packet type byte to start of data
        byte[] data = Fbb.SizedByteArray().PrependByteArray((byte)Constants.PacketType.PlayerFired);

        SendToLobby(data);
    }

    public static void SendPlayerDeflected(bool active)
    {
        // Write data
        Fbb.Clear();
        var unit = UnitDeflect.CreateUnitDeflect(Fbb, active);
        Fbb.Finish(unit.Value);
        // Add packet type byte to start of data
        byte[] data = Fbb.SizedByteArray().PrependByteArray((byte)Constants.PacketType.PlayerDeflected);

        SendToLobby(data);
    }

    // Send data packet to each other player in the lobby
    private static void SendToLobby(byte[] data)
    {
        if (Client.Instance == null) return;

        // Test this packet on the test network player (123) if it exists
        PacketHandler.HandlePacket(123, data);

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
     * Can be overridden i.e. NetworkEvents.OnUnitPosition = (args) => {...}.
     */
    public static Action<ulong, UnitPosition> OnUnitPosition = (steamId, args) =>
    {
        var senderPlayer = GameData.Instance.ClientPlayers.Find(player => player.PlayerId == steamId);

        if (senderPlayer)
        {
            senderPlayer.Player.LerpMovement.StartMoving(new Vector2(args.X, args.Y));
        }
    };

    public static Action<ulong> OnPlayerSpawn = (steamId) =>
    {
        GameData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, steamId));
    };

    public static Action<ulong, UnitFire> OnUnitFire = (steamId, args) =>
    {
        var senderPlayer = GameData.Instance.ClientPlayers.Find(player => player.PlayerId == steamId);

        if (senderPlayer)
        {
            senderPlayer.Player.FireGun(new Vector2(args.X, args.Y), new Vector2(args.MouseX, args.MouseY), args.IsSpecial, false);
        }
    };

    public static Action<ulong, UnitDeflect> OnUnitDeflect = (steamId, args) =>
    {
        var senderPlayer = GameData.Instance.ClientPlayers.Find(player => player.PlayerId == steamId);

        if (senderPlayer)
        {
            senderPlayer.Player.Block(args.Active, false);
        }
    };
}
