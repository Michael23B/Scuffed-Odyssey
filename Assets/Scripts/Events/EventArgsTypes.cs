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

public class PlayerFiredEventArgs : EventArgs
{
    public PlayerFiredEventArgs(ulong steamId, float x, float y, float mouseX, float mouseY, bool isSpecial)
    {
        SteamId = steamId;
        X = x;
        Y = y;
        MouseX = mouseX;
        MouseY = mouseY;
        IsSpecial = isSpecial;
    }
    public ulong SteamId { get; }
    public float X { get; }
    public float Y { get; }
    public float MouseX { get; }
    public float MouseY { get; }
    public bool IsSpecial { get; }
}