using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public float bulletSpeedModifier = 10f;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject specialBullet;

    private float cooldown = 0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void FireBullet(Vector2 origin, Vector2 target, bool isSpecial)
    {
        if (cooldown < 0)
        {
            animator.SetTrigger("fireGun");
            Vector2 direction = target - origin;
            direction.Normalize();
            GameObject projectile = Instantiate(isSpecial ? specialBullet : bullet, origin, Quaternion.identity);
            projectile.SetActive(true); // needed for for when enemy has learnt
            projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier, isSpecial);
            cooldown = animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}
