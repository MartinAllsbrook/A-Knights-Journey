using UnityEngine;

public class RidingPlayer : MonoBehaviour
{
    public static RidingPlayer Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}