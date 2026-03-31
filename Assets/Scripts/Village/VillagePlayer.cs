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

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "EnterSwordplay":
                VillageManager.Instance.ShowEnterMessage(SceneTag.Swordplay);
                break;
            case "EnterArchery":
                VillageManager.Instance.ShowEnterMessage(SceneTag.Archery);
                break;
            case "EnterRiding":
                VillageManager.Instance.ShowEnterMessage(SceneTag.Riding);
                break;
            case "EnterTournament":
                VillageManager.Instance.ShowEnterMessage(SceneTag.Tournament);
                break;
            default:
                Debug.Log("Player entered unknown trigger: " + collision.tag);
                break;
        }
    }
}
