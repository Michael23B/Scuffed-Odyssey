using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public float MovePercentagePerFrame = 0.25f;

    private bool isMoving;
    private Vector3 difference;
    private Vector3 target;
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (!isMoving) return;

        difference = target - transform.position;

        if (difference.magnitude < 0.01f)
        {
            StopMoving();
            return;
        }

        if (difference.magnitude > 5)
        {
            player.Move(difference.x, difference.y, false);
            StopMoving();
            return;
        }

        player.Move(difference.x * MovePercentagePerFrame, difference.y * MovePercentagePerFrame, false);
    }

    public void StartMoving(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        player.StopMoving();
    }
}