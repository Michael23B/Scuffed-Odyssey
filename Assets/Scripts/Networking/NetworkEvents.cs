using System;
using Facepunch.Steamworks;
using FlatBuffers;
using UnityEngine;

public static class NetworkEvents
{
    static readonly FlatBufferBuilder Fbb = new FlatBufferBuilder(1);

    // TODO refactor to unit position
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
        // TODO refactor into unit spawned
        byte[] data = {(byte) Constants.PacketType.PlayerSpawned};
        SendToLobby(data);
    }

    public static void SendPlayerFired(Vector2 origin, Vector2 target, bool isSpecial)
    {
        // Write data
        Fbb.Clear();
        var unit = UnitFire.CreateUnitFire(Fbb, UnitType.Player, origin.x, origin.y, target.x, target.y, isSpecial);
        Fbb.Finish(unit.Value);
        // Add packet type byte to start of data
        byte[] data = Fbb.SizedByteArray().PrependByteArray((byte)Constants.PacketType.UnitFired);

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

    public static void SendEnemySpawned(Enemy enemy)
    {
        // Write data
        Fbb.Clear();
        var unit = UnitSpawned.CreateUnitSpawned(Fbb, UnitType.Enemy, enemy.Id, enemy.transform.position.x, enemy.transform.position.y);
        Fbb.Finish(unit.Value);
        // Add packet type byte to start of data
        byte[] data = Fbb.SizedByteArray().PrependByteArray((byte)Constants.PacketType.UnitSpawned);

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
        GameData.Instance.ClientPlayers.TryGetValue(steamId, out var sender);

        if (sender)
        {
            sender.Player.LerpMovement.StartMoving(new Vector2(args.X, args.Y));
        }
    };

    // TODO refactor into unit spawned
    public static Action<ulong> OnPlayerSpawn = (steamId) =>
    {
        GameData.Instance.ClientPlayers[123] = NetworkPlayer.CreateNetworkPlayer(false, steamId);
    };

    public static Action<ulong, UnitFire> OnUnitFire = (steamId, args) =>
    {
        GameData.Instance.ClientPlayers.TryGetValue(steamId, out var sender);

        if (sender)
        {
            sender.Player.FireGun(new Vector2(args.X, args.Y), new Vector2(args.MouseX, args.MouseY), args.IsSpecial, false);
        }
    };

    public static Action<ulong, UnitDeflect> OnUnitDeflect = (steamId, args) =>
    {
        GameData.Instance.ClientPlayers.TryGetValue(steamId, out var sender);

        if (sender)
        {
            sender.Player.Block(args.Active, false);
        }
    };

    public static Action<ulong, UnitSpawned> OnUnitSpawned = (steamId, args) =>
    {
        Enemy enemy = Enemy.CreateEnemy(args.Id);
        enemy.transform.position = new Vector2(args.X, args.Y);
    };
}
