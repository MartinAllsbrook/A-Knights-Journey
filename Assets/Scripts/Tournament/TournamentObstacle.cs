using System;
using UnityEngine;

public enum ObstacleType
{
    Target,
    Dummy
}

[RequireComponent(typeof(Collider2D))]
public class TournamentObstacle : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] ObstacleType type;

    public event Action OnDestroyed = delegate { };

    public ObstacleType Type => type;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDestroy();
        }
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke();
        OnDestroyed = null;
        Destroy(gameObject);
    }
}
