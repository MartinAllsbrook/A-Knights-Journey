using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public void Collect()
    {
        // Play collection sound, animation, etc. here

        Destroy(gameObject);
    }
}