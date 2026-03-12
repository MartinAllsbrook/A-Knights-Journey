using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Managed Scene Names")]
    [SerializeField] private string archerySceneName = "ArcheryGame";
    [SerializeField] private string swordplaySceneName = "SwordplayGame";
    [SerializeField] private string ridingSceneName = "RidingGame";
    [SerializeField] private string villageSceneName = "Village";

    private string currentGameplayScene;
    private bool isTransitioning;

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

    void Start()
    {
        if (string.IsNullOrEmpty(currentGameplayScene))
        {
            EnterVillage();
        }
    }

    public string CurrentGameplayScene => currentGameplayScene;

    public void SetInitialGameplayScene(string sceneName)
    {
        currentGameplayScene = sceneName;
    }

    public void EnterVillage()
    {
        if (isTransitioning) return;
        SwitchScene(villageSceneName);
    }

    public void EnterArcheryGame()
    {
        if (isTransitioning) return;
        SwitchScene(archerySceneName);
    }

    public void EnterSwordplayGame()
    {
        if (isTransitioning) return;
        SwitchScene(swordplaySceneName);
    }

    public void EnterRidingGame()
    {
        if (isTransitioning) return;
        SwitchScene(ridingSceneName);
    }

    public void SwitchScene(string sceneName)
    {
        isTransitioning = true;
        currentGameplayScene = sceneName;
        SceneManager.LoadScene(sceneName);
        isTransitioning = false;
    }
}