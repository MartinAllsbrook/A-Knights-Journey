using UnityEngine;

public class CrowdCharacter : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] crowdSprites;
    [SerializeField] float bobbingSpeed = 2f;
    [SerializeField] float bobbingAmount = 0.1f;
    [SerializeField] float bobbingTime = 0.2f;

    Vector2 initialPosition;
    float bobbingPhase;

    void Start()
    {
        if (crowdSprites.Length > 0)
        {
            spriteRenderer.sprite = crowdSprites[Random.Range(0, crowdSprites.Length)];
        }

        initialPosition = transform.position;
        bobbingPhase = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // snappy
        // float bobbing = Mathf.Sin(Time.time * bobbingSpeed + bobbingPhase);

        // if (bobbing > bobbingTime)
        //     transform.position = initialPosition + new Vector2(0, bobbingAmount);
        // else if (bobbing <= bobbingTime)
        //     transform.position = initialPosition;

        // smooth
        // float bobbing = Mathf.Sin(Time.time * bobbingSpeed + bobbingPhase) * bobbingAmount;
        // transform.position = initialPosition + new Vector2(0, bobbing);

        float bobbing = Mathf.Sin(Time.time * bobbingSpeed + bobbingPhase) * bobbingAmount;
        float interval = 0.125f / 2f; // Adjust this for more or less snappiness
        bobbing = Mathf.Round(bobbing / interval) * interval;
        transform.position = initialPosition + new Vector2(0, bobbing);
    }
}
