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
        GameObject projectile = Instantiate(bullet, origin, Quaternion.identity);
        projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeedModifier;
    }
}
