using System.Collections.Generic;
using System.Text;
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

        // Setup callbacks
        Client.Instance.Lobby.OnLobbyCreated = (success) =>
        {
            Debug.Log($"Lobby Created? {success}");
            if (success)
            {
                // Create local player
                if (!localPlayer)
                {
                    localPlayer = CreateNetworkPlayer(true, Client.Instance.SteamId);
                }
            }
        };

        Client.Instance.Lobby.OnLobbyJoined = (success) =>
        {
            Debug.Log($"Lobby Joined? {success}");
            if (success)
            {
                var data = Encoding.UTF8.GetBytes("spawn"); // Spawn packet

                if (!localPlayer)
                {
                    localPlayer = CreateNetworkPlayer(true, Client.Instance.SteamId);
                }

                // Create a player for each other player in the lobby
                foreach (var memberId in Client.Instance.Lobby.GetMemberIDs())
                {
                    if (memberId != Client.Instance.SteamId)
                    {
                        clientPlayers.Add(CreateNetworkPlayer(false, memberId));

                        // Send this player a packet to spawn a player for you on their screen
                        Client.Instance.Networking.SendP2PPacket(memberId, data, data.Length);
                    }
                }

                // Send a message to other players to create a player with my id
            }
        };

        Client.Instance.LobbyList.OnLobbiesUpdated = () =>
        {
            if (Client.Instance.LobbyList.Finished)
            {
                Debug.Log($"Found {Client.Instance.LobbyList.Lobbies.Count} lobbies");

                foreach (LobbyList.Lobby lobby in Client.Instance.LobbyList.Lobbies)
                {
                    Debug.Log($"Found Lobby: {lobby.Name}");
                }
            }

        };

        Client.Instance.Networking.OnP2PData = (steamid, bytes, length, channel) =>
        {
            var str = Encoding.UTF8.GetString(bytes, 0, length);

            // Spawn packet, create a NetworkPlayer for the new player
            if (str == "spawn")
            {
                clientPlayers.Add(CreateNetworkPlayer(false, steamid));
                return;
            }

            var senderPlayer = clientPlayers.Find(player => player.PlayerId == steamid);

            if (senderPlayer)
            {
                string[] data = str.Split('?');
                float senderX = float.Parse(data[0]);
                float senderY = float.Parse(data[1]);

                senderPlayer.transform.position = new Vector3(senderX, senderY);
            }
        };

        Client.Instance.Networking.SetListenChannel(0, true);
    }

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
