using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    protected MinigameController minigameController;

    protected virtual void Awake()
    {
        minigameController = FindAnyObjectByType<MinigameController>();
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