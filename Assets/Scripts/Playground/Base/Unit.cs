using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    [SerializeField] protected float bulletSpeedModifier = 10f;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject specialBullet;

    protected void FireBullet(Vector2 origin, Vector2 target, bool isSpecial)
    {
        Vector2 direction = target - origin;
        direction.Normalize();
        GameObject projectile = Instantiate(isSpecial ? specialBullet : bullet, origin, Quaternion.identity);
        projectile.SetActive(true); // needed for for when enemy has learnt
        projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier, isSpecial);
    }

    public abstract void HandleDamamge(GameObject bullet);
}
