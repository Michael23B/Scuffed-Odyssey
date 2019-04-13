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

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(keyRight))
        {
            rgb.velocity += new Vector2(force, 0);
        }

        if (Input.GetKey(keyLeft))
        {
            rgb.velocity += new Vector2(-force, 0);
        }

        if (Input.GetKey(keyUp))
        {
            rgb.velocity += new Vector2(0, force);
        }

        if (Input.GetKey(keyDown))
        {
            rgb.velocity += new Vector2(0, -force);
        }
    }
}
