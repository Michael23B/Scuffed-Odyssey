using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] float bulletSpeedModifier = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject specialBullet;

    protected void FireBullet(Vector2 origin, Vector2 target, bool isSpecial)
    {
        Vector2 direction = target - origin;
        direction.Normalize();
        GameObject projectile = Instantiate(isSpecial ? specialBullet : bullet, origin, Quaternion.identity);
        projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier);
    }
}
