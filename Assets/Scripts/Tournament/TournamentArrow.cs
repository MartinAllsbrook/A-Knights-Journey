using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TournamentArrow : MonoBehaviour
{
    Rigidbody2D rb;

    float velocity = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = transform.right * velocity;
    }

    public void Set(float velocity)
    {
        this.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TournamentArcheryObstacle"))
        {
            TournamentObstacle obstacle = collision.collider.GetComponent<TournamentObstacle>();
            if (obstacle != null)
            {
                obstacle.TakeDamage(20f);
            }
        }

        // Destroy the arrow on impact
        Destroy(gameObject);
    }
}