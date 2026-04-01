using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TournamentResult : MonoBehaviour
{
    [SerializeField] Image contestantSprite;
    [SerializeField] Image medalSprite;
    [SerializeField] TextMeshProUGUI placeText;
    [SerializeField] Image playerBorder;

    public void Set(Sprite contestant, bool dnf, bool isPlayer = false)
    {
        Debug.Log($"Setting result - Contestant: {contestant.name}, DNF: {dnf}, IsPlayer: {isPlayer}");
        contestantSprite.sprite = contestant;
        if (dnf)
        {
            placeText.text = "DNF";
            medalSprite.gameObject.SetActive(false);
        }
        if (isPlayer)
        {
            playerBorder.gameObject.SetActive(true);
        }
    }
}