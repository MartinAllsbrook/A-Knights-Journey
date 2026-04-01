using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TournamentResult
{
    public Image contestantSprite;
    public Image medalSprite;
    public TextMeshProUGUI placeText;
}

public class TournamentScoreboard : MonoBehaviour
{
    [SerializeField] TournamentResult[] results;

    public void SetResults(Contestant[] contestants)
    {
        for (int i = 0; i < contestants.Length; i++)
        {
            results[i].contestantSprite.sprite = contestants[i].GetComponent<SpriteRenderer>().sprite;
            if (contestants[i].dnf)
            {
                results[i].placeText.text = "DNF";
                results[i].medalSprite.gameObject.SetActive(false);
            }
        }
    }
}
