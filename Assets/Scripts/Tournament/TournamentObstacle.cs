using System;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Slider heathBar;

    public event Action OnDestroyed = delegate { };

    public ObstacleType Type => type;

    public void TakeDamage(float damage)
    {
        health -= damage;

        heathBar.gameObject.SetActive(true);
        heathBar.value = health / 100f;

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
