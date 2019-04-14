using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyPessApplyMotion : MonoBehaviour
{
    [SerializeField] float speedModifier = 0.1f;

    private Rigidbody2D rgb;
    private Vector2 velocity;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * speedModifier;
        float vertical = Input.GetAxisRaw("Vertical") * speedModifier;
        rgb.MovePosition(new Vector2(rgb.position.x + horizontal, rgb.position.y + vertical));
    }
}
