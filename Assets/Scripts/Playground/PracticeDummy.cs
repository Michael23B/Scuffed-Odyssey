using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PracticeDummy : Unit
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject targetObject;
    [SerializeField] float shootCooldown = 2f;
    [SerializeField] float bulletSpeedModifier = 10f;
    [SerializeField] int oddsOfMutatingIncrement = 10;

    private int oddsOfMutating = 0;
    private Rigidbody2D rgb;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > shootCooldown)
        {
            time = 0f;
            FireBullet(new Vector2(transform.position.x, transform.position.y), targetObject.transform.position);
        }
    }

    override public void HandleDamamge(GameObject incomingBullet)
    {
        Bullet bulletInstance = incomingBullet.GetComponent<Bullet>();
        health -= bulletInstance.bulletDmg;
        if (bulletInstance.isSpecial && oddsOfMutatingIncrement > 0)
        {
            oddsOfMutating += oddsOfMutatingIncrement;
            Debug.Log("Enemy is learning");
            if (Random.Range(0, 100) <= oddsOfMutating)
            {
                bullet = incomingBullet.gameObject;
                bullet.SetActive(false);
                oddsOfMutatingIncrement = -1;
                Debug.Log("Enemy has learnt");
                return;
            }
        }
        Destroy(incomingBullet);
    }

    private void FireBullet(Vector2 origin, Vector2 target)
    {
        Vector2 direction = target - origin;
        direction.Normalize();
        GameObject projectile = Instantiate(bullet, origin, Quaternion.identity);
        projectile.SetActive(true); // needed for for when enemy has learnt
        projectile.GetComponent<Bullet>().InitProps(gameObject, bulletSpeedModifier, direction * bulletSpeedModifier, false);
    }
}
