using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float moveInput = 0f;

    void OnEnable()
    {
        InputManager.Archery_OnMove += HandleArcheryMove;
    }

    void OnDisable()
    {
        InputManager.Archery_OnMove -= HandleArcheryMove;
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
    }

    private void HandleArcheryMove(float value)
    {
        moveInput = value;
    }
}