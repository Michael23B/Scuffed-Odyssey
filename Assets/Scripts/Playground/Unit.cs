using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] float bulletSpeedModifier = 10f;
    [SerializeField] GameObject bullet;

    protected void FireBullet(Vector2 origin, Vector2 target)
    {
        Vector2 direction = target - origin;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject projectile = Instantiate(bullet, origin, rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeedModifier;
    }
}
