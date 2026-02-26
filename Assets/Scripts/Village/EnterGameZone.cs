using Unity.VisualScripting;
using UnityEngine;

enum GameType
{
    Archery,
    Swordplay,
    Riding
}

public class EnterGameZone : MonoBehaviour
{
    [InspectorTextArea] [SerializeField] private string message;

    [SerializeField] private GameType gameToEnter;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(message);
            switch (gameToEnter)
            {
                case GameType.Archery:
                    GameManager.Instance.EnterArcheryGame();
                    break;
                case GameType.Swordplay:
                    GameManager.Instance.EnterSwordplayGame();
                    break;
                case GameType.Riding:
                    GameManager.Instance.EnterRidingGame();
                    break;
            }
        }
    }
}
