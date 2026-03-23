using System.Collections;
using UnityEngine;

public class Contestant : MonoBehaviour
{
    [SerializeField] float speedScaler = 0.5f;
    [SerializeField] Animator horseAnimator;
    [SerializeField] TournamentArrow arrowPrefab;

    // Stats
    float riding = 5f;
    float archery = 5f;
    float swordplay = 5f;

    bool stopped = false;


    void Start()
    {
        speedScaler = Random.Range(0.4f, 0.6f);
        horseAnimator.SetBool("IsMoving", true);
    }

    void Update()
    {
        if (stopped)
            return;

        transform.Translate(Time.deltaTime * riding * speedScaler * Vector3.right);

        // Check for obstacles in front
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 4f);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (hit.collider.CompareTag("TournamentArcheryObstacle"))
            {
                TournamentObstacle obstacle = hit.collider.GetComponent<TournamentObstacle>();
                if (obstacle != null)
                {
                    StartCoroutine(DealWithArcheryObstacle(obstacle));
                }
            }
            if (hit.collider.CompareTag("TournamentSwordplayObstacle"))
            {
                TournamentObstacle obstacle = hit.collider.GetComponent<TournamentObstacle>();
                if (obstacle != null)
                {
                    float distanceToObstacle = Vector2.Distance(transform.position, hit.point);
                    
                    if (distanceToObstacle < 1f)
                        StartCoroutine(DealWithSwordplayObstacle(obstacle));
                }
            }
        }
    }

    void Stop()
    {
        stopped = true;
        horseAnimator.SetBool("IsMoving", false);
    }

    void Move()
    {
        stopped = false;
        horseAnimator.SetBool("IsMoving", true);
    }

    IEnumerator DealWithArcheryObstacle(TournamentObstacle target)
    {
        Stop();

        float arrowFireRate = 10f / archery;

        while (target != null)
        {
            FireArrow();
            yield return new WaitForSeconds(arrowFireRate);
        }

        Move();
    }

    void FireArrow()
    {
        if (arrowPrefab != null)
        {
            TournamentArrow newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            newArrow.Set(archery);
        }
    }

    IEnumerator DealWithSwordplayObstacle(TournamentObstacle target )
    {
        Stop();

        float attackRate = 10f / swordplay;

        Attack(target);

        while (target != null)
        {
            yield return new WaitForSeconds(attackRate);
            Attack(target);
        }

        Move();
    }

    void Attack(TournamentObstacle target)
    {
        if (target != null)
        {
            target.TakeDamage(swordplay);
        }
    }

}
