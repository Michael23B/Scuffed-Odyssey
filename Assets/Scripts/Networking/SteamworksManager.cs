using Facepunch.Steamworks;
using UnityEngine;

public class SteamworksManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);

        Config.ForUnity(Application.platform.ToString());

        new Client(480); // Create a new FacePunch.Steamworks.Client

        if (Client.Instance == null)
        {
            Debug.LogError("Steam not initialized");
            return;
        }

        // Setup our callback methods
        SteamworksCallbacks.Initialize();
        // Listen for P2P messages on channel 0
        Client.Instance.Networking.SetListenChannel(0, true);
    }

    private void OnDestroy()
    {
        Client.Instance?.Dispose();
    }

    void Update()
    {
        Client.Instance?.Update();
    }
}
