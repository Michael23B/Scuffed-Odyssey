using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rgb;
    private Animator animator;
    private float dodgeCooldown = 0f;

    private void Start()
    {
        player = GetComponent<Player>();
        rgb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dodgeCooldown <= 0)
            {
                player.Move(horizontal * 1.5f, vertical * 1.5f, false);
                dodgeCooldown = 1f;
            }
            else Debug.Log("Boy is your face red");
        }
        else
        {
            if (horizontal != 0 || vertical != 0)
            {
                player.Move(horizontal, vertical);
            }
            else player.StopMoving();

            if (Input.GetMouseButtonDown(0))
            {
                player.FireGun(player.bulletSpawn.transform.position, Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                player.FireGun(player.bulletSpawn.transform.position, Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player.Block(false);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                player.Block(true);
            }
        }

        if (dodgeCooldown > 0)
        {
            dodgeCooldown -= Time.deltaTime;
        }
        player.RotateGun(Input.mousePosition.x, Input.mousePosition.y);
    }
}
