using Facepunch.Steamworks;
using UnityEngine;

public class SteamworksManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);

        Config.ForUnity(Application.platform.ToString());

        if (Client.Instance == null)
        {
            new Client(480); // Create a new FacePunch.Steamworks.Client
        }

        if (Client.Instance == null)
        {
            Debug.LogError("Steam not initialized");
            return;
        }

        // Setup callbacks
        Client.Instance.Lobby.OnLobbyCreated = (success) =>
        {
        };

        Client.Instance.Lobby.OnLobbyJoined = (success) =>
        {
            if (success)
            {
                // TODO create a player and set it to local player

                // Send a message to other players to create a player with my id

                // Create a player for each other player in the lobby using their id
            }
            else Debug.Log("Failed to join lobby");
        };

        Client.Instance.LobbyList.OnLobbiesUpdated = () =>
        {
            Debug.Log("Lobbies updating");
            if (Client.Instance.LobbyList.Finished)
            {
                Debug.Log("Lobbies finished updating");
                Debug.Log($"Found {Client.Instance.LobbyList.Lobbies.Count} lobbies");

                foreach (LobbyList.Lobby lobby in Client.Instance.LobbyList.Lobbies)
                {
                    Debug.Log($"Found Lobby: {lobby.Name}");
                }
            }

        };
    }

    public void CreateLobby()
    {
        Client.Instance.Lobby.Create(Lobby.Type.Public, 10);
    }

    public void JoinLobby()
    {
        if (Client.Instance.LobbyList.Lobbies.Count > 0)
        {
            Debug.Log($"Joining lobby: {Client.Instance.LobbyList.Lobbies[0].Name}");
            Client.Instance.Lobby.Join(Client.Instance.LobbyList.Lobbies[0].LobbyID);
            return;
        }
        Debug.Log("No lobby to join");
    }

    public void FindLobbies()
    {
        Client.Instance.LobbyList.Refresh();
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
