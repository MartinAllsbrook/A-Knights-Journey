using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class Movement : MonoBehaviour
{
    [Header("Basic Options")]
    [SerializeField] float reactivity  = 10f;
    [SerializeField] bool lockYAxis = false;

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

        if (lockYAxis)
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetVelocity(Vector2 velocity)
    {
        if (lockYAxis)
            velocity.y = 0;

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