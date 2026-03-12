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
            VillageManager.Instance.ShowEnterGameMessage(gameToEnter);
        }
    }
}
