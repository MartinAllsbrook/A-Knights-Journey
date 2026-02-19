using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rider : MonoBehaviour
{
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] float verticalMoveSpeed = 5f;

    float maxHorizontalOffset = 10f;
    float maxVerticalOffset = 7f;

    Vector2 input;
    Vector2 offset = Vector2.zero;

    Rigidbody2D riderRigidbody;

    void Awake()
    {
        riderRigidbody = GetComponent<Rigidbody2D>();
        riderRigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
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

    void FixedUpdate()
    {
        if (RidingPlayer.Instance == null)
            return;

        Vector2 cameraPosition = RidingPlayer.Instance.transform.position;

        offset += Vector2.right * input.x * horizontalMoveSpeed * Time.fixedDeltaTime;
        offset += Vector2.up * input.y * verticalMoveSpeed * Time.fixedDeltaTime;
        
        offset.x = Mathf.Clamp(offset.x, -maxHorizontalOffset, maxHorizontalOffset);
        offset.y = Mathf.Clamp(offset.y, -maxVerticalOffset, maxVerticalOffset);

        Vector2 targetPosition = cameraPosition + offset;
        riderRigidbody.MovePosition(targetPosition);
    }

    void OnMove(Vector2 moveInput)
    {
        input = moveInput;
    }
}
