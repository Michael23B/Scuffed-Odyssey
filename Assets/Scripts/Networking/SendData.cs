using System;
using System.Text;
using UnityEngine;
using Facepunch.Steamworks;

public class SendData : MonoBehaviour
{
    private void Update()
    {
        if (Client.Instance == null) return;

        float x = transform.position.x;
        float y = transform.position.y;
        // TODO dodgy way of doing it for now it's fine
        var data = Encoding.UTF8.GetBytes($"{Math.Round(x, 2)}?{Math.Round(y, 2)}");

        // Send x,y coordinates to each person in this lobby
        foreach (var memberId in Client.Instance.Lobby.GetMemberIDs())
        {
            if (memberId != Client.Instance.SteamId)
            {
                Client.Instance.Networking.SendP2PPacket(memberId, data, data.Length);
            }
        }
    }
}
