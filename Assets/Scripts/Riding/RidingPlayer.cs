using UnityEngine;

public class RidingPlayer : MonoBehaviour
{
    public static RidingPlayer Instance { get; private set; }

    [Header("References")]
    [SerializeField] Transform spriteTransform;

    [Header("Riding Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedIncreaseRate = 0.05f;
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] float verticalMoveSpeed = 5f;
    [SerializeField] float maxHorizontalOffset = 8f;
    [SerializeField] float maxVerticalOffset = 4f;

    Vector2 input;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

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

        moveSpeed += moveSpeedIncreaseRate * Time.deltaTime;

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 clampedPosition = spriteTransform.localPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxHorizontalOffset, maxHorizontalOffset);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -maxVerticalOffset, maxVerticalOffset);
        spriteTransform.localPosition = clampedPosition;
    }

    void OnMove(Vector2 moveInput)
    {
        input = moveInput;
    }
}