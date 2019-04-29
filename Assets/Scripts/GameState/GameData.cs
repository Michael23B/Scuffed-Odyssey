using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public NetworkPlayer LocalPlayer { get; set; }
    public Dictionary<ulong, NetworkPlayer> ClientPlayers { get; set; } = new Dictionary<ulong, NetworkPlayer>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        DontDestroyOnLoad(this);
    }
}
