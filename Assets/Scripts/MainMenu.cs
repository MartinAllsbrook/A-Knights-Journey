using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        GameManager.Instance.EnterScene(SceneTag.Village);
    }
}
