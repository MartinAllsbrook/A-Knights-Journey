using UnityEngine;

public class Contestant : MonoBehaviour
{
    [SerializeField] float speedScaler = 0.5f;
    [SerializeField] Animator horseAnimator;

    // Stats
    float riding = 5f;
    float archery = 5f;
    float swordplay = 5f;

    void Start()
    {
        horseAnimator.SetBool("IsMoving", true);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * riding * speedScaler * Vector3.right);
    }
}
