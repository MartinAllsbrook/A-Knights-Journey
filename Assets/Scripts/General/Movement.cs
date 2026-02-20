using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class Movement : MonoBehaviour
{
    [SerializeField] float ractivity = 10f;

    Vector2 targetVelocity;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public void SetVelocity(Vector2 targetVelocity)
    {
        this.targetVelocity = targetVelocity;
    }

    void FixedUpdate()
    {
        Vector2 force = (targetVelocity - rb.linearVelocity) * ractivity;
        rb.AddForce(force, ForceMode2D.Force);
    }
}