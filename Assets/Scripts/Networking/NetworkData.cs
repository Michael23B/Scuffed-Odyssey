using System.Collections.Generic;
using UnityEngine;

public class NetworkData : MonoBehaviour
{
    public static NetworkData Instance { get; private set; }

    public NetworkPlayer LocalPlayer { get; set; }
    public List<NetworkPlayer> ClientPlayers { get; set; } = new List<NetworkPlayer>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        DontDestroyOnLoad(this);
    }
}
