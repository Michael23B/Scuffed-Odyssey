using Facepunch.Steamworks;
using UnityEngine;

public class SendPlayerPosition : MonoBehaviour
{
    public float UpdateRate = 0.01f;
    [SerializeField] private UnitType unitType = UnitType.Player;
    private float nextUpdate;
    private Vector2 prevPosition;

    private void Start()
    {
        // TODO set function to either send player or enemy position and call that in update
    }

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
