using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifespan = 4f;

    private Vector2 originalVelocity;
    private GameObject firer;
    private float bulletSpeedModifier;
    private Vector2 startingPos;
    private Rigidbody2D rgb;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitProps(GameObject firer, float bulletSpeedModifier, Vector2 velocity)
    {
        rgb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        this.firer = firer;
        this.bulletSpeedModifier = bulletSpeedModifier;
        originalVelocity = velocity;
        rgb.velocity = velocity;
        startingPos = firer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Ricochet(collision.contacts[0].normal);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Deflect")
            DelfectBullet();
    }

    private void DelfectBullet()
    {
        Vector2 direction = (Vector2)firer.transform.position - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        originalVelocity = direction * bulletSpeedModifier;
        rgb.velocity = direction * bulletSpeedModifier;
    }

    private void Ricochet(Vector2 normal)
    {
        Vector2 reflectVelocity = Vector2.Reflect(originalVelocity, normal);
        originalVelocity = reflectVelocity;
        rgb.velocity = reflectVelocity;
    }
}
