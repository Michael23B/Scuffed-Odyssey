using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject bulletSpawn;

    [SerializeField] float moveSpeedModifier = 0.1f;
    public GameObject blockHitBox;
    private GameObject blockHitBoxInstantiated;
    private Rigidbody2D rgb;
    private Animator animator;
    private Vector2 velocity;
    public LerpMovement LerpMovement { get; private set; }
    public Gun Gun { get; private set; }

    public void Initialize()
    {
        rgb = GetComponent<Rigidbody2D>();
        LerpMovement = GetComponent<LerpMovement>();
        Gun = gun.GetComponent<Gun>();
        animator = GetComponent<Animator>();
        blockHitBoxInstantiated = Instantiate(blockHitBox, rgb.position, Quaternion.identity);
        blockHitBoxInstantiated.SetActive(false);
    }

    void Start()
    {
        Initialize();
    }

    override public void HandleDamamge(GameObject bullet)
    {
        health -= bullet.GetComponent<Bullet>().bulletDmg;
        Destroy(bullet);
    }

    public void Move(float x, float y, bool applyMoveSpeed = true)
    {
        animator.SetBool("playerWalking", true);

        float modifier = applyMoveSpeed ? moveSpeedModifier : 1;
        Vector2 newPos = new Vector2(rgb.position.x + x * modifier, rgb.position.y + y * modifier);
        rgb.MovePosition(newPos);
    }

    public void StopMoving()
    {
        animator.SetBool("playerWalking", false);
    }

    public void FireGun(Vector2 origin, Vector2 target, bool isSpecial, bool fireEvent = true)
    {
        Gun.FireBullet(origin, target, isSpecial);
        if (fireEvent)
        {
            NetworkEvents.SendPlayerFired(origin, target, isSpecial);
        }
    }

    public void Block(bool active)
    {
        blockHitBoxInstantiated.SetActive(active);
        if (active)
        {
            blockHitBoxInstantiated.GetComponent<Rigidbody2D>().MovePosition(new Vector2(2, 0) + (Vector2)transform.position);
        }
    }

    public void RotateGun(float mouseX, float mouseY)
    {
        // Distance from camera to object.  We need this to get the proper calculation.
        float camDis = Camera.main.transform.position.y - gun.gameObject.transform.position.y;

        // Get the mouse position in world space. Using camDis for the Z axis.
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, camDis));
        //gameObject.transform.LookAt(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        Vector3 target = new Vector3(mouse.x, mouse.y, 0);
        gun.transform.right = target - transform.position;
    }
}
