using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    [SerializeField] float moveSpeedModifier = 0.1f;
    public GameObject blockHitBox;

    private GameObject blockHitBoxInstantiated;
    private float dodgeCooldown = 0f;
    private Animator animator;
    private Rigidbody2D rgb;
    private Vector2 velocity;
    public LerpMovement LerpMovement { get; private set; }

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LerpMovement = GetComponent<LerpMovement>();
        blockHitBoxInstantiated = Instantiate(blockHitBox, rgb.position, Quaternion.identity);
        blockHitBoxInstantiated.SetActive(false);
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
            if (horizontal != 0 || vertical != 0)
            {
                animator.SetBool("playerWalking", true);
                Vector2 newPos = new Vector2(rgb.position.x + horizontal * moveSpeedModifier, rgb.position.y + vertical * moveSpeedModifier);
                rgb.MovePosition(newPos);
            }
            else animator.SetBool("playerWalking", false);

            if (Input.GetMouseButtonDown(0))
            {
                FireBullet(new Vector2(transform.position.x, transform.position.y), Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                FireBullet(new Vector2(transform.position.x, transform.position.y), Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                blockHitBoxInstantiated.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                blockHitBoxInstantiated.SetActive(true);
                blockHitBoxInstantiated.GetComponent<Rigidbody2D>().MovePosition(new Vector2(2, 0) + (Vector2)transform.position);
            }
        }

        if (dodgeCooldown > 0)
            dodgeCooldown -= Time.deltaTime;
    }

    override public void HandleDamamge(GameObject bullet)
    {
        health -= bullet.GetComponent<Bullet>().bulletDmg;
        Destroy(bullet);
    }
}
