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
            Target target = collision.GetComponent<Target>();
            if (target != null)
            {
                target.RetractTarget(); 
            }

            ArcheryController.Instance.TallyHit();
            ArcheryController.Instance.AddScore(1); // Award points for hitting a target
        }

        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                coin.Collect(); 
                ArcheryController.Instance.CollectCoin(); 
            }

            ArcheryController.Instance.TallyHit();
        }

        ArcheryController.Instance.TallyShot();
        Destroy(gameObject);
    }
}