using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifespan = 4f;

    private GameObject firer;
    private float bulletSpeedModifier;
    private Vector2 startingPos;
    private Rigidbody2D rgb;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public void InitProps(GameObject firer, float bulletSpeedModifier)
    {
        this.firer = firer;
        this.bulletSpeedModifier = bulletSpeedModifier;
        startingPos = firer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Deflect")
            DelfectBullet();
        else if (collision.tag == "Wall")
        {
            ContactPoint2D[] contacts = new ContactPoint2D[2];
            collision.GetContacts(contacts);
            Ricochet(contacts[0].normal);
        }
    }

    private void DelfectBullet()
    {
        Vector2 direction = (Vector2)firer.transform.position - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeedModifier;
    }

    private void Ricochet(Vector2 normal)
    {
        Vector2 reflect = Vector2.Reflect(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(1, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = reflect;
    }
}
