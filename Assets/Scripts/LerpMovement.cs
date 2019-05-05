using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public float MovePercentagePerFrame = 0.25f;

    private bool isMoving;
    private Vector3 difference;
    private Vector3 target;
    private Unit unit;

    void Awake()
    {
        unit = GetComponent<Unit>();
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
            unit.Move(difference.x, difference.y, false);
            StopMoving();
            return;
        }

        unit.Move(difference.x * MovePercentagePerFrame, difference.y * MovePercentagePerFrame, false);
    }

    public void StartMoving(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        unit.StopMoving();
    }
}