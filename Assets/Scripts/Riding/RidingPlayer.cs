using UnityEngine;

public class RidingPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform spriteTransform;


    [Header("Riding Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] float verticalMoveSpeed = 5f;
    [SerializeField] float maxHorizontalOffset = 8f;
    [SerializeField] float maxVerticalOffset = 4f;

    Vector2 input;

    void OnEnable()
    {
        InputManager.Riding_OnMove += OnMove;
    }

    void OnDisable()
    {
        InputManager.Riding_OnMove -= OnMove;
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        spriteTransform.Translate(Vector2.right * input.x * horizontalMoveSpeed * Time.deltaTime);
        spriteTransform.Translate(Vector2.up * input.y * verticalMoveSpeed * Time.deltaTime);

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 clampedPosition = spriteTransform.localPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxHorizontalOffset, maxHorizontalOffset);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -maxVerticalOffset, maxVerticalOffset);
        spriteTransform.localPosition = clampedPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            RidingGame.Instance.PlayerHit();
        }
    }

    void OnMove(Vector2 moveInput)
    {
        input = moveInput;
    }
}