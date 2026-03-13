using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    protected MinigameController minigameController;

    protected virtual void Awake()
    {
        minigameController = FindFirstObjectByType<MinigameController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                coin.Collect();
                minigameController.CollectCoin();
            }
        }
    }
}