using UnityEngine;

class SwordplayPlayerController : MonoBehaviour
{
    [Header("Sword References")]
    [SerializeField] GameObject upSword;
    [SerializeField] GameObject downSword;
    [SerializeField] GameObject leftSword;
    [SerializeField] GameObject rightSword;

    [Header("Swordplay Settings")]
    [SerializeField] float attackDuration = 0.15f;

    float attackTimer = 0f;

    void OnEnable()
    {
        InputManager.Swordplay_AttackUp += OnAttackUp;
        InputManager.Swordplay_AttackDown += OnAttackDown;
        InputManager.Swordplay_AttackLeft += OnAttackLeft;
        InputManager.Swordplay_AttackRight += OnAttackRight;
    }

    void OnDisable()
    {
        InputManager.Swordplay_AttackUp -= OnAttackUp;
        InputManager.Swordplay_AttackDown -= OnAttackDown;
        InputManager.Swordplay_AttackLeft -= OnAttackLeft;
        InputManager.Swordplay_AttackRight -= OnAttackRight;
    }

    void Start()
    {
        TurnOffAllSwords();
    }

    void Update()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                TurnOffAllSwords();
            }
        }
    }

    void OnAttackUp()
    {
        TurnOffAllSwords();
        upSword.SetActive(true);
        attackTimer = attackDuration;
        Debug.Log("Swordplay: Attack Up");
    }

    void OnAttackDown()
    {
        TurnOffAllSwords();
        downSword.SetActive(true);
        attackTimer = attackDuration;
        Debug.Log("Swordplay: Attack Down");
    }

    void OnAttackLeft()
    {
        TurnOffAllSwords();
        leftSword.SetActive(true);
        attackTimer = attackDuration;
        Debug.Log("Swordplay: Attack Left");
    }

    void OnAttackRight()
    {
        TurnOffAllSwords();
        rightSword.SetActive(true);
        attackTimer = attackDuration;
        Debug.Log("Swordplay: Attack Right");
    }

    void TurnOffAllSwords()
    {
        upSword.SetActive(false);
        downSword.SetActive(false);
        leftSword.SetActive(false);
        rightSword.SetActive(false);
    }
}