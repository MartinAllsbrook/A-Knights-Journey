using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movement))]
public class RidingPlayer : MinigamePlayer
{
    [SerializeField] float horizontalMoveSpeed = 5f;
    [SerializeField] DashSlider dashSlider;

    [Header("Dash")]
    [SerializeField] float dashCooldown = 2f;

    float maxHorizontalOffset = 10f;
    float maxVerticalOffset = 7f;

    bool canDash = true;

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
        InputManager.Riding_OnDash += OnDash;
    }

    void OnDisable()
    {
        InputManager.Riding_OnMove -= OnMove;   
        InputManager.Riding_OnDash -= OnDash;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            RidingController.Instance.PlayerHit();
        }
    }

    void LateUpdate()
    {
        Vector2 velocity = Vector2.zero;
        velocity += Vector2.right * (input.x * horizontalMoveSpeed);

        movement.SetVelocity(velocity);
    }

    void OnMove(float moveInput)
    {
        input = new Vector2(moveInput, input.y);
    }

    void OnDash()
    {
        if (canDash)
        {
            movement.Dash();
            canDash = false;
            StartCoroutine(DashCooldownRoutine());
        }
    }

    IEnumerator DashCooldownRoutine()
    {
        float cooldown = dashCooldown;
        float elapsed = 0f;

        while (elapsed < cooldown)
        {
            elapsed += Time.deltaTime;
            dashSlider.SetFill(elapsed / cooldown);
            yield return null;
        }

        canDash = true;
        dashSlider.SetFill(1f);
    }

}
