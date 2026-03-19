using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class Movement : MonoBehaviour
{
    [Header("Basic Movement")]
    [SerializeField] float reactivity  = 10f;

    [Header("Dash")]
    [SerializeField] float dashMultiplier = 6f;
    [SerializeField] float dashDuration = 0.15f;

    bool isDashing = false;

    Vector2 targetVelocity;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public void SetVelocity(Vector2 velocity)
    {
        if (isDashing)
            velocity *= dashMultiplier;

        targetVelocity = velocity;
    }

    void FixedUpdate()
    {
        Vector2 force = (targetVelocity - rb.linearVelocity) * reactivity;
        rb.AddForce(force, ForceMode2D.Force);
    }

    public void Dash()
    {
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        isDashing = true;
        
        SetVelocity(targetVelocity); // Apply dash multiplier immediately

        yield return new WaitForSeconds(dashDuration);
        
        isDashing = false;
    }
}