using System;
using FlatBuffers;
using PacketType = Constants.PacketType;

public static class PacketHandler
{
    // Deserializes a packet and calls the appropriate NetworkEvent method
    public static void HandlePacket(ulong steamid, byte[] bytes)
    {
        // Packet type is the first byte of the packet
        PacketType packetType = (PacketType)bytes[0];
        // The remaining bytes are data
        ByteBuffer buffer = new ByteBuffer(1);
        if (bytes.Length > 1)
        {
            byte[] data = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, data, 0, data.Length);
            buffer = new ByteBuffer(data);
        }

        // Construct class from data and call network event based on packet type
        switch (packetType)
        {
            case PacketType.PlayerPosition:
                NetworkEvents.OnUnitPosition(steamid, UnitPosition.GetRootAsUnitPosition(buffer));
                break;
            case PacketType.PlayerSpawned:
                NetworkEvents.OnPlayerSpawn(steamid);
                break;
            case PacketType.PlayerFired:
                NetworkEvents.OnUnitFire(steamid, UnitFire.GetRootAsUnitFire(buffer));
                break;
            default:
                throw new Exception($"Could not read packet type {packetType}");
        }
    }
}
