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
}
