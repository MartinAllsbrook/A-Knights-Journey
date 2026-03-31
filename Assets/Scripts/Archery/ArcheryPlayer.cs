using UnityEngine;

[RequireComponent(typeof(Movement))] 
public class ArcheryPlayer : MinigamePlayer
{
    [SerializeField] float moveSpeed;
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Transform firePoint;
    
    Movement movement;
    
    float moveInput = 0f;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void OnEnable()
    {
        InputManager.Archery_OnMove += HandleMove;
        InputManager.Archery_OnFire += HandleFire;
    }

    void OnDisable()
    {
        InputManager.Archery_OnMove -= HandleMove;
        InputManager.Archery_OnFire -= HandleFire;
    }

    void Update()
    {
        movement.SetVelocity(new Vector2(moveInput * moveSpeed, 0f));
    }

    private void HandleMove(float value)
    {
        moveInput = value;
    }

    private void HandleFire()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}