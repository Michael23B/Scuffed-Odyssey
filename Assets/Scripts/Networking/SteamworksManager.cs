using System.Collections.Generic;
using Facepunch.Steamworks;
using UnityEngine;

public class SteamworksManager : MonoBehaviour
{
    private NetworkPlayer localPlayer;
    private List<NetworkPlayer> clientPlayers = new List<NetworkPlayer>();

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
    }

    // TODO move to own class with UI methods
    public void CreateLobby()
    {
        Client.Instance.Lobby.Leave();
        Client.Instance.Lobby.Create(Lobby.Type.Public, 10);
    }

    public void JoinLobby()
    {
        if (Client.Instance.LobbyList.Lobbies.Count > 0)
        {
            // Join the most recently created lobby
            Client.Instance.Lobby.Join(Client.Instance.LobbyList.Lobbies[Client.Instance.LobbyList.Lobbies.Count - 1].LobbyID);
            return;
        }
        Debug.Log("No lobby to join");
    }

    public void FindLobbies()
    {
        Client.Instance.LobbyList.Refresh();
    }

    public void LeaveLobby()
    {
        Client.Instance.Lobby.Leave();
        Debug.Log("Left Lobby");
    }

    private NetworkPlayer CreateNetworkPlayer(bool isLocalPlayer, ulong playerId)
    {
        GameObject player = Instantiate(FindObjectOfType<PrefabHelper>().PlayerPrefab);
        NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
        networkPlayer.InitializePlayer(isLocalPlayer, playerId);
        return networkPlayer;
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
