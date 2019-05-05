using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int health = 10;

    public abstract void HandleDamamge(GameObject bullet);
    public abstract void Move(float x, float y, bool applyMovementSpeed = true);
    public abstract void StopMoving();
}