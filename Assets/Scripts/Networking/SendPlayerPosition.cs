using Facepunch.Steamworks;
using UnityEngine;

public class SendPlayerPosition : MonoBehaviour
{
    private void Update()
    {
        if (Client.Instance == null) return;

        float x = transform.position.x;
        float y = transform.position.y;

        NetworkEvents.SendPlayerPosition(x, y);
    }
}
