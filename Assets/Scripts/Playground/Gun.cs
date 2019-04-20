using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public float bulletSpeedModifier = 10f;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject specialBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireBullet(Vector2 origin, Vector2 target, bool isSpecial)
    {
        Vector2 direction = target - origin;
        direction.Normalize();
        GameObject projectile = Instantiate(isSpecial ? specialBullet : bullet, origin, Quaternion.identity);
        projectile.SetActive(true); // needed for for when enemy has learnt
        projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier, isSpecial);
    }
}
