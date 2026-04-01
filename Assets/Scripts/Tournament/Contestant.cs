using System.Collections;
using UnityEngine;

public class Contestant : MonoBehaviour
{
    [SerializeField] float speedScaler = 5f;
    [SerializeField] float swordplayScaler = 5f;
    [SerializeField] float archeryScaler = 5f;

    [SerializeField] Animator horseAnimator;
    [SerializeField] TournamentArrow arrowPrefab;
    [SerializeField] Transform bowTransform;
    [SerializeField] Transform swordTransform;
    [SerializeField] TrailRenderer swordTrail;

    [SerializeField] LayerMask obstacleLayer;

    int maxLevel = 100;

    float riding = 5f;
    float archery = 5f;
    float swordplay = 5f;

    bool stopped = false;
    bool finished = false;

    public bool dnf = false;


    void Start()
    {
        horseAnimator.SetBool("IsMoving", true);
    }

    void Update()
    {
        if (stopped)
            return;

        transform.Translate(Time.deltaTime * riding * speedScaler * Vector3.right);

        // Check for obstacles in front
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 4f, obstacleLayer);

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

    public void Set(float riding, float archery, float swordplay, bool isPlayer = false)
    {
        this.riding = riding;
        this.archery = archery;
        this.swordplay = swordplay;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish Line"))
        {
            Debug.Log($"{gameObject.name} finished!");
            Finished();
        }
    }

    public void Finished()
    {
        if (finished)
            return;

        finished = true;
        TournamentController.Instance.ContestantFinished(this);
        StartCoroutine(StopInAMoment(2f));
    }

    IEnumerator StopInAMoment(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopAllCoroutines();
        Stop();
    }

    #region Archery Obstacle

    IEnumerator DealWithArcheryObstacle(TournamentObstacle target)
    {
        target.OnDestroyed += () => {
            StopAllCoroutines();
            ExitArcheryState();
        };

        float arrowFireRate = 1 / (archeryScaler * archery);

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
    }

    #endregion

    #region Swordplay Obstacle

    IEnumerator DealWithSwordplayObstacle(TournamentObstacle target )
    {
        target.OnDestroyed += () => {
            StopAllCoroutines();
            ExitSwordplayState();
        };

        float attackDuration = 0.4f;
        float attackRate = 1 / (swordplayScaler * swordplay) - attackDuration;
        if (attackRate < 0f)
            attackRate = 0f;

        Stop();

        while (target != null)
        {
            yield return StartCoroutine(Attack(attackDuration));
            target.TakeDamage(20f);
    
            yield return new WaitForSeconds(attackRate);
        }

        ExitSwordplayState();
    }


    void ExitSwordplayState()
    {
        Move();
        swordTransform.gameObject.SetActive(false);
    }

    IEnumerator Attack(float attackDuration)
    {        
        swordTransform.gameObject.SetActive(true);

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
