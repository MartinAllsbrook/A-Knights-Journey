using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movement))]
public class RidingPlayer : MinigamePlayer
{
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] float forwardMoveSpeed = 5f;
    [SerializeField] float backwardMoveSpeed = 1f;

    float maxHorizontalOffset = 10f;
    float maxVerticalOffset = 7f;

    Vector2 input;
    Vector2 offset = Vector2.zero;
    Movement movement;

    protected override void Awake()
    {
        base.Awake();

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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
        if (collision.CompareTag("Obstacle"))
        {
            RidingController.Instance.PlayerHit();
        }
    }

    void LateUpdate()
    {
        Vector2 velocity = Vector2.zero;
        velocity += Vector2.right * (input.x * horizontalMoveSpeed);

        if (input.y > 0)
            velocity += Vector2.up * (input.y * forwardMoveSpeed);
        else if (input.y < 0)
            velocity += Vector2.up * (input.y * (RidingController.Instance.MoveSpeed + backwardMoveSpeed));

        movement.SetVelocity(velocity);
    }

    void OnMove(Vector2 moveInput)
    {
        input = moveInput;
    }
}
