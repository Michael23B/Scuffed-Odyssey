using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public float MovePercentagePerFrame = 0.25f;
    private Rigidbody2D rb;
    private bool isMoving;
    private Vector3 difference;
    private Vector3 target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isMoving) return;

        difference = target - transform.position;

        if (difference.magnitude < 0.1)
        {
            StopMoving();
            return;
        }

        if (difference.magnitude > 2)
        {
            rb.MovePosition(target);
            StopMoving();
            return;
        }

        rb.MovePosition(transform.position + difference * 0.1f);
    }

    public void StartMoving(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}