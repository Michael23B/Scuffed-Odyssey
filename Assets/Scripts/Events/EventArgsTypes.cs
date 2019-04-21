using System;

public class PlayerPositionEventArgs : EventArgs
{
    public PlayerPositionEventArgs(ulong steamId, float x, float y)
    {
        SteamId = steamId;
        X = x;
        Y = y;
    }
    public ulong SteamId { get; }
    public float X { get; }
    public float Y { get; }
}

public class PlayerSpawnedEventArgs : EventArgs
{
    public PlayerSpawnedEventArgs(ulong steamId)
    {
        SteamId = steamId;
    }
    public ulong SteamId { get; }
}

public class PlayerFireEventArgs : EventArgs
{
    public PlayerFireEventArgs(ulong steamId, float x, float y, int bulletType)
    {
        SteamId = steamId;
        X = x;
        Y = y;
        BulletType = bulletType;
    }
    public ulong SteamId { get; }
    public float X { get; }
    public float Y { get; }
    public float BulletType { get; }
}