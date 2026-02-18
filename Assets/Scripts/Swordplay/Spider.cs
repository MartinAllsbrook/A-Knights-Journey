using Unity.VisualScripting;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("Spider Settings")]
    [SerializeField] float moveSpeed = 2f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwordplayGame.Instance.PlayerHit();
            Destroy(gameObject);
        }

        if (collision.CompareTag("Sword"))
        {
            SwordplayGame.Instance.EnemyDefeated();
            Destroy(gameObject);
        }
    }
}
