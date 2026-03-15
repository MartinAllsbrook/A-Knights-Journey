using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image[] hearts;

    void Awake()
    {
        if (hearts.Length != 3)
        {
            Debug.LogError("HealthBar: There should be exactly 3 heart images assigned.");
        }

        UpdateHealthBar(3); // Initialize with full health
    }

    public void UpdateHealthBar(int health)
    {
        if (health < 0 || health > hearts.Length)
        {
            Debug.LogError("HealthBar: Health value must be between 0 and " + hearts.Length);
            return;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < health; // Enable heart if it's within the current health
        }
    }
}
