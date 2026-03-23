using System.Collections;
using UnityEngine;

public class Contestant : MonoBehaviour
{
    [SerializeField] float speedScaler = 0.5f;
    [SerializeField] Animator horseAnimator;
    [SerializeField] TournamentArrow arrowPrefab;
    [SerializeField] Transform bowTransform;
    [SerializeField] Transform swordTransform;
    [SerializeField] TrailRenderer swordTrail;

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

    #region Archery Obstacle

    IEnumerator DealWithArcheryObstacle(TournamentObstacle target)
    {
        target.OnDestroyed += () => {
            StopAllCoroutines();
            ExitArcheryState();
        };

        float arrowFireRate = 10f / archery;

        EnterArcheryState();

        FireArrow();
        while (target != null)
        {
            yield return new WaitForSeconds(arrowFireRate);            
            FireArrow();
        }

        // Backup in case the event doesn't trigger for some reason
        ExitArcheryState();
    }

    void EnterArcheryState()
    {
        Stop();
        bowTransform.gameObject.SetActive(true);
    }

    void ExitArcheryState()
    {
        Move();
        bowTransform.gameObject.SetActive(false);
    }

    void FireArrow()
    {
        TournamentArrow newArrow = Instantiate(arrowPrefab, bowTransform.position, Quaternion.identity);
        newArrow.Set(archery);
    }

    #endregion

    #region Swordplay Obstacle

    IEnumerator DealWithSwordplayObstacle(TournamentObstacle target )
    {
        target.OnDestroyed += () => {
            StopAllCoroutines();
            ExitSwordplayState();
        };

        float attackRate = 10f / swordplay;

        Stop();


        while (target != null)
        {
            yield return StartCoroutine(Attack());
            target.TakeDamage(20f);

            yield return new WaitForSeconds(0.25f);
        }

        ExitSwordplayState();
    }


    void ExitSwordplayState()
    {
        Move();
        swordTransform.gameObject.SetActive(false);
    }

    IEnumerator Attack()
    {        
        swordTransform.gameObject.SetActive(true);

        float attackDuration = 0.4f;
        float startAngle = 45f;
        float endAngle = -30f;
        float elapsed = 0f;


        swordTransform.localRotation = Quaternion.Euler(0f, 0f, startAngle);
        swordTrail.Clear();

        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / attackDuration);
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            swordTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        swordTransform.gameObject.SetActive(false);
    }

    #endregion
}
