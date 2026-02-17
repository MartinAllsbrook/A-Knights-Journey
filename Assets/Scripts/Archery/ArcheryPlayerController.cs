using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Transform firePoint;
    float moveInput = 0f;

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
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
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