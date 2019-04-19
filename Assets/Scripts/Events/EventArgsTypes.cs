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