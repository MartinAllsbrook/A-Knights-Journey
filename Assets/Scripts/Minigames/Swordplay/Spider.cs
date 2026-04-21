using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Spider : MonoBehaviour
{
    [SerializeField] float minMoveDistance = 1f;
    [SerializeField] SpriteRenderer spiderSprite;

    float moveSpeed = 2f; // Sey by Controller
    float moveDuration = 1f; // Sey by Controller

    Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            Vector2 playerPosition = SwordplayPlayer.instance.transform.position;
            Vector2 difference = playerPosition - (Vector2)transform.position;

            Vector2 moveDirection;

            // If Player is more horizontal than vertical, move horizontally towards player
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                moveDirection = new Vector2(Mathf.Sign(difference.x), 0);
    
            // Else, move vertically towards player
            else
                moveDirection = new Vector2(0, Mathf.Sign(difference.y));
            
            // Rotate to face movement direction
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90);

            movement.SetVelocity(moveDirection * moveSpeed);
        
            // Wait for moveDuration, then repeat
            yield return new WaitForSeconds(moveDuration);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        moveDuration = minMoveDistance / moveSpeed;

        StartCoroutine(MoveRoutine());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            HitPlayer(collider.gameObject);
        }

        if (collider.CompareTag("Sword"))
        {
            Kill();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {         
            HitPlayer(collision.collider.gameObject);
        }
    }

    void HitPlayer(GameObject player)
    {        
        SwordplayPlayer playerScript = player.GetComponent<SwordplayPlayer>();
        playerScript.PlayDamagedEffects();
        
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator PlayHitEffects()
    {
        spiderSprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spiderSprite.color = Color.white;
        Destroy(gameObject);
    }

    void Kill()
    {
        SwordplayController.Instance.EnemyDefeated();
        StopAllCoroutines();
        movement.SetVelocity(Vector2.zero);
        StartCoroutine(PlayHitEffects());
    }
}
