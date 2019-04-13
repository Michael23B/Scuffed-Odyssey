using System;
using System.Text;
using UnityEngine;
using Facepunch.Steamworks;

public class SendData : MonoBehaviour
{
    void Start()
    {
        if (Client.Instance == null)
        {
            new Client(480); // Create a new FacePunch.Steamworks.Client
        }

        Client.Instance.Networking.OnP2PData = (steamid, bytes, length, channel) =>
        {
            var str = Encoding.UTF8.GetString(bytes, 0, length);
            Console.WriteLine("Got: " + str);
        };

        Client.Instance.Networking.SetListenChannel(0, true);
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        var data = Encoding.UTF8.GetBytes($"{x}?{y}");
        Client.Instance.Networking.SendP2PPacket(Client.Instance.Lobby.CurrentLobby, data, data.Length);
    }
}
