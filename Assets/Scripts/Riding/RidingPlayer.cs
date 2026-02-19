using UnityEngine;

public class RidingPlayer : MonoBehaviour
{
    public static RidingPlayer Instance { get; private set; }

    [Header("Riding Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedIncreaseRate = 0.05f;

    public float MoveSpeed => moveSpeed;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        moveSpeed += moveSpeedIncreaseRate * Time.deltaTime;
    }
}