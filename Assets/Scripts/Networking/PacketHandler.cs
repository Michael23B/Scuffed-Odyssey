using System;
using System.Text;
using UnityEngine;

public static class PacketHandler
{
    // Deserializes a packet and calls the appropriate NetworkEvent method
    public static void HandlePacket(ulong steamid, byte[] bytes, int length)
    {
        string buffer = Encoding.UTF8.GetString(bytes, 0, length);

        // properties[0] == packet type
        // properties[1-9] == values
        string[] properties = buffer.Split(Constants.NetworkPacketDelimiter);

        // Check that packet contains valid packet type
        if (!int.TryParse(properties[0], out var packetType))
        {
            return;
        }

        // Call network event based on packet type
        switch (packetType)
        {
            case (int)Constants.PacketType.PlayerPosition:
                NetworkEvents.OnPlayerPosition(new PlayerPositionEventArgs(
                    steamid,
                    float.Parse(properties[1]),
                    float.Parse(properties[2])
                    )
                );
                break;
            case (int)Constants.PacketType.PlayerSpawned:
                NetworkEvents.OnPlayerSpawn(new PlayerSpawnedEventArgs(steamid));
                break;
            case (int)Constants.PacketType.PlayerFired:
                NetworkEvents.OnPlayerFire(new PlayerFiredEventArgs(
                    steamid,
                    float.Parse(properties[1]),
                    float.Parse(properties[2]), 
                    float.Parse(properties[3]),
                    float.Parse(properties[4]),
                    bool.Parse(properties[5])
                    )
                );
                break;
            default:
                throw new Exception($"Could not read packet type {properties[0]}");
        }
    }

    // Serializes arguments into {packet type}{delimiter}{value}. Example packet: 1;0.75;4;test
    public static byte[] SerializePacket(Constants.PacketType packetType, string[] args = null)
    {
        StringBuilder sb = new StringBuilder($"{(int)packetType}");

        if (args != null)
        {
            foreach (string str in args)
            {
                sb.Append(Constants.NetworkPacketDelimiter);
                sb.Append(str);
            }
        }
        
        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
