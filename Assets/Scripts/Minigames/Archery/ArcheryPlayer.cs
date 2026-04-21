using UnityEngine;

[RequireComponent(typeof(Movement))] 
public class ArcheryPlayer : MinigamePlayer
{
    [SerializeField] float moveSpeed;
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    
    Movement movement;
    
    float moveInput = 0f;

    bool isFiring = false;
    float fireCooldown = 0f;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
    }

    void OnEnable()
    {
        InputManager.Archery_OnMove += HandleMove;
        InputManager.Archery_OnFireDown += HandleFireDown;
        InputManager.Archery_OnFireUp += HandleFireUp;
    }

    void OnDisable()
    {
        InputManager.Archery_OnMove -= HandleMove;
        InputManager.Archery_OnFireDown -= HandleFireDown;
        InputManager.Archery_OnFireUp -= HandleFireUp;
    }

    void Update()
    {
        movement.SetVelocity(new Vector2(moveInput * moveSpeed, 0f));

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;

        if (isFiring && fireCooldown <= 0f)
        {
            FireArrow();
            fireCooldown = fireRate;
        }
    }

    private void HandleMove(float value)
    {
        moveInput = value;
    }

    private void HandleFireDown()
    {
        isFiring = true;
    }

    private void HandleFireUp()
    {
        isFiring = false;
    }

    private void FireArrow()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}