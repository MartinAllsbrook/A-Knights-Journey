using UnityEngine;

[RequireComponent(typeof(Movement))]
public class VillagePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void OnEnable()
    {
        InputManager.Village_OnMove += OnMove;
    }

    void OnDisable()
    {
        InputManager.Village_OnMove -= OnMove;
    }
        
    void OnMove(Vector2 moveInput)
    {
        Vector2 velocity = moveInput * moveSpeed;
        movement.SetVelocity(velocity);
    }
}
