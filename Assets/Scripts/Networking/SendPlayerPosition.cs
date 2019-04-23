using Facepunch.Steamworks;
using UnityEngine;

public class SendPlayerPosition : MonoBehaviour
{
    public float UpdateRate = 0.01f;
    private float nextUpdate;
    private Vector2 prevPosition;

    private void Update()
    {
        if (Client.Instance == null || Time.time < nextUpdate || transform.position.Equals(prevPosition)) return;

        float x = transform.position.x;
        float y = transform.position.y;
        nextUpdate = Time.time + UpdateRate;
        prevPosition = transform.position;

        NetworkEvents.SendPlayerPosition(x, y);
    }
}
