using UnityEngine;

public class TournamentController : MonoBehaviour
{
    [SerializeField] int playerIndex = 0;
    [SerializeField] Contestant[] contestants;

    void Start()
    {
        if (playerIndex < contestants.Length)
        {
        }
    }
}
