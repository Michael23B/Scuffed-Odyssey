public struct Constants
{
    public static char NetworkPacketDelimiter = ';';

    public enum PacketType: byte
    {
        PlayerPosition,
        PlayerSpawned,
        PlayerFired,
        PlayerDeflected
    }
}


