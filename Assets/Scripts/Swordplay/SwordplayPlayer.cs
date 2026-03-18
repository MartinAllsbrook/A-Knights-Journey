using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
class SwordplayPlayer : MinigamePlayer
{
    [Header("Sword References")]
    [SerializeField] GameObject sword;

    [SerializeField] GameObject nIndicator;
    [SerializeField] GameObject neIndicator;
    [SerializeField] GameObject eIndicator;
    [SerializeField] GameObject seIndicator;
    [SerializeField] GameObject sIndicator;
    [SerializeField] GameObject swIndicator;
    [SerializeField] GameObject wIndicator;
    [SerializeField] GameObject nwIndicator;

    [Header("Settings")]
    [SerializeField] float attackDuration = 0.15f;
    [SerializeField] float movementSpeed = 5f;

    float attackTimer = 0f;

    Vector2 movementInput;
    Movement movement;

    string direction = "";

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
    }

    void OnEnable()
    {
        InputManager.Swordplay_Attack += OnAttack;
        InputManager.Swordplay_OnMove += OnMoveInput;
    }

    void OnDisable()
    {
        InputManager.Swordplay_Attack -= OnAttack;
        InputManager.Swordplay_OnMove -= OnMoveInput;
    }

    void OnMoveInput(Vector2 input)
    {
        movementInput = input;
        movement.SetVelocity(movementInput * movementSpeed);

        string newDirection = GetDirectionFromInput(input);
        if (newDirection != "")
            direction = newDirection;
            
        ShowAttackIndicator(direction);
    }

    string GetDirectionFromInput(Vector2 input)
    {
        if (input == Vector2.zero) return "";

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        if (angle >= 337.5f || angle < 22.5f) return "Right";
        if (angle >= 22.5f && angle < 67.5f) return "UpRight";
        if (angle >= 67.5f && angle < 112.5f) return "Up";
        if (angle >= 112.5f && angle < 157.5f) return "UpLeft";
        if (angle >= 157.5f && angle < 202.5f) return "Left";
        if (angle >= 202.5f && angle < 247.5f) return "DownLeft";
        if (angle >= 247.5f && angle < 292.5f) return "Down";
        if (angle >= 292.5f && angle < 337.5f) return "DownRight";

        return "";
    }

    void ShowAttackIndicator(string direction)
    {
        Debug.Log("Swordplay: Move " + direction);
        nIndicator.SetActive(direction == "Up");
        neIndicator.SetActive(direction == "UpRight");
        eIndicator.SetActive(direction == "Right");
        seIndicator.SetActive(direction == "DownRight");
        sIndicator.SetActive(direction == "Down");
        swIndicator.SetActive(direction == "DownLeft");
        wIndicator.SetActive(direction == "Left");
        nwIndicator.SetActive(direction == "UpLeft");
    }

    void OnAttack()
    {
        if (direction != "")
        {
            Attack(direction);
        }
    }

    void Attack(string direction)
    {
        Debug.Log("Swordplay: Attack " + direction);
        float startAngle, endAngle;

        switch (direction)
        {
            case "Up":
                // Flipped and offset
                startAngle = 45f;
                endAngle = -45f;
                break;
            case "UpRight":
                startAngle = 0f;
                endAngle = -90f;
                break;
            case "Right":
                // Flipped
                startAngle = -45f;
                endAngle = -135f;
                break;
            case "DownRight":
                startAngle = -90f;
                endAngle = -180f;
                break;
            case "Down":
                // Flipped and offset
                startAngle = -135f;
                endAngle = -225f;
                break;
            case "DownLeft":
                startAngle = 180f;
                endAngle = 90f;
                break;
            case "Left":
                // Flipped
                startAngle = 135f;
                endAngle = 45f;
                break;
            case "UpLeft":
                startAngle = 90f;
                endAngle = 0f;
                break;
            default:
                return;
        }

        StartCoroutine(Attack(startAngle, endAngle));
    }
    
    IEnumerator Attack(float startAngle, float endAngle)
    {
        sword.SetActive(true);
        sword.transform.localRotation = Quaternion.Euler(0f, 0f, startAngle);

        float elapsed = 0f;
        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / attackDuration);
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            sword.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        sword.SetActive(false);
    }

}