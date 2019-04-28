using Facepunch.Steamworks;
using UnityEngine;

public class UIMethods : MonoBehaviour
{
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
            Client.Instance.Lobby.Join(Client.Instance.LobbyList.Lobbies[0].LobbyID);
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

    public void CreateDummyNetworkTest()
    {
        GameData.Instance.ClientPlayers.Add(NetworkPlayer.CreateNetworkPlayer(false, 123));
        Debug.Log("Created network test dummy. Copies the inputs you send to other clients and displays them to you.");
    }
}
