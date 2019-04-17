using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifespan = 4f;

    public GameObject firer;
    public float bulletSpeedModifier;

    private Rigidbody2D rgb;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
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
        if (collision.tag == "Deflect") {
            RedirectBullet();
        }
    }

    private void RedirectBullet()
    {
        Vector2 direction = (Vector2)firer.transform.position - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeedModifier;
    }
}
