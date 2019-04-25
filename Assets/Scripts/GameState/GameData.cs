using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public NetworkPlayer DummyNetworkPlayer { get; set; }
    public NetworkPlayer LocalPlayer { get; set; }
    public List<NetworkPlayer> ClientPlayers { get; set; } = new List<NetworkPlayer>();

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        DontDestroyOnLoad(this);
        // Temporary code for testing networking events
//        DummyNetworkPlayer = NetworkPlayer.CreateNetworkPlayer(false, 123);
    }
}
