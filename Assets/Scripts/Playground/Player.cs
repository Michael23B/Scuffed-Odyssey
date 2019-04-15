using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeedModifier = 0.1f;
    [SerializeField] float bulletSpeedModifier = 10f;
    public GameObject attackHitBox;
    public GameObject bullet;

    private GameObject attackHitBoxInstantiated;
    private float dodgeCooldown = 0f;

    private Rigidbody2D rgb;
    private Vector2 velocity;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        attackHitBoxInstantiated = Instantiate(attackHitBox, rgb.position, Quaternion.identity);
        attackHitBoxInstantiated.SetActive(false);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (dodgeCooldown <= 0)
            {
                Vector2 newPos = new Vector2(rgb.position.x + horizontal * 1.5f, rgb.position.y + vertical * 1.5f);
                rgb.MovePosition(newPos);
                dodgeCooldown = 1f;
            }
            else Debug.Log("Boy is your face red");
        }
        else
        {
            Vector2 newPos = new Vector2(rgb.position.x + horizontal * moveSpeedModifier, rgb.position.y + vertical * moveSpeedModifier);
            rgb.MovePosition(newPos);

            if (Input.GetMouseButtonDown(0))
            {
                FireBullet();
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                attackHitBoxInstantiated.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                attackHitBoxInstantiated.SetActive(true);
                attackHitBoxInstantiated.GetComponent<Rigidbody2D>().MovePosition(new Vector2(2, 0) + newPos);
            }
        }

        if (dodgeCooldown > 0)
            dodgeCooldown -= Time.deltaTime;
    }

    private void FireBullet()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject projectile = (GameObject)Instantiate(bullet, myPos, rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeedModifier;
    }
}
