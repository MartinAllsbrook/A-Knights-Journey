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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Game Zone"))
        {
            Debug.Log("Entered game zone: " + collision.gameObject.name);    
        }
    }
        
    void OnMove(Vector2 moveInput)
    {
        Vector2 velocity = moveInput * moveSpeed;
        movement.SetVelocity(velocity);
    }
}
