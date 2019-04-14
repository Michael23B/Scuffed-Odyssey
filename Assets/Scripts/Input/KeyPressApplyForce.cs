using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyPressApplyForce : MonoBehaviour
{
    [SerializeField] float force = 0f;
    [SerializeField] KeyCode keyLeft = KeyCode.None;
    [SerializeField] KeyCode keyRight = KeyCode.None;
    [SerializeField] KeyCode keyUp = KeyCode.None;
    [SerializeField] KeyCode keyDown = KeyCode.None;

    private Rigidbody2D rgb;
    private Vector2 velocity;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capture input in Update since it's more responsive
        if (Input.GetKey(keyRight))
        {
            velocity += new Vector2(force, 0);
        }

        if (Input.GetKey(keyLeft))
        {
            velocity += new Vector2(-force, 0);
        }

        if (Input.GetKey(keyUp))
        {
            velocity += new Vector2(0, force);
        }

        if (Input.GetKey(keyDown))
        {
            velocity += new Vector2(0, -force);
        }
    }

    void FixedUpdate()
    {
        // Perform physics changes in FixedUpdate
        rgb.velocity += velocity;
        velocity = new Vector2();
    }
}
