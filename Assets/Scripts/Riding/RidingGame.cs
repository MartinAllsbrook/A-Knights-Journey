using UnityEngine;

class RidingGame : MonoBehaviour
{
    public static RidingGame Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerHit()
    {
        Debug.Log("Player hit an obstacle!");
    }
}