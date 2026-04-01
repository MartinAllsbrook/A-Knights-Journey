using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TournamentScoreboard : MonoBehaviour
{
    [SerializeField] TournamentResult[] results;
    [SerializeField] Button continueButton;
    [SerializeField] TextMeshProUGUI titleText;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    void OnContinueClicked()
    {
        GameManager.Instance.EnterScene(SceneTag.Village);
    }

    public void SetResults(Contestant[] contestants)
    {
        for (int i = 0; i < contestants.Length; i++)
        {
            results[i].Set(contestants[i].contestantSprite, contestants[i].dnf, contestants[i].isPlayer);
        }
    }
}
