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
        if (Input.GetKeyDown(KeyCode.X)) {
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                Instantiate(bullet, newPos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity += new Vector2(horizontal * bulletSpeedModifier, vertical * bulletSpeedModifier);
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
}
