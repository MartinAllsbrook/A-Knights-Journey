using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movement))]
public class Rider : MonoBehaviour
{
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] float forwardMoveSpeed = 5f;
    [SerializeField] float backwardMoveSpeed = 1f;

    float maxHorizontalOffset = 10f;
    float maxVerticalOffset = 7f;

    Vector2 input;
    Vector2 offset = Vector2.zero;
    Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void OnEnable()
    {
        InputManager.Riding_OnMove += OnMove;
    }

    void OnDisable()
    {
        InputManager.Riding_OnMove -= OnMove;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            RidingGame.Instance.PlayerHit();
        }
    }

    void LateUpdate()
    {
        Vector2 velocity = Vector2.zero;
        velocity += Vector2.right * (input.x * horizontalMoveSpeed);

        if (input.y > 0)
            velocity += Vector2.up * (input.y * forwardMoveSpeed);
        else if (input.y < 0)
            velocity += Vector2.up * (input.y * (RidingGame.Instance.MoveSpeed + backwardMoveSpeed));

        movement.SetVelocity(velocity);
    }

    void OnMove(Vector2 moveInput)
    {
        input = moveInput;
    }
}
