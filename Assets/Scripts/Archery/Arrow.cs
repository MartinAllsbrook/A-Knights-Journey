using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            ArcheryGameController.Instance.TallyHit();
            ArcheryGameController.Instance.AddScore(1); // Award points for hitting a target
        }
        // Handle collision logic here (e.g., damage, destroy arrow, etc.)
        ArcheryGameController.Instance.TallyShot();
        Destroy(gameObject);
    }
}