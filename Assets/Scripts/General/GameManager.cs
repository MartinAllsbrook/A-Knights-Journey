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
        RequestSceneTransition(villageSceneName);
    }

    public void EnterArcheryGame()
    {
        RequestSceneTransition(archerySceneName);
    }

    public void EnterSwordplayGame()
    {
        RequestSceneTransition(swordplaySceneName);
    }

    public void EnterRidingGame()
    {
        RequestSceneTransition(ridingSceneName);
    }

    private void RequestSceneTransition(string targetScene)
    {
        if (isTransitioning)
        {
            Debug.LogWarning("Scene transition already in progress.");
            return;
        }

        if (string.IsNullOrWhiteSpace(targetScene) || !IsManagedScene(targetScene))
        {
            Debug.LogError($"Scene '{targetScene}' is not a managed gameplay scene.");
            return;
        }

        if (currentGameplayScene == targetScene)
        {
            Debug.Log($"Already in scene '{targetScene}'.");
            return;
        }

        if (!CanTransition(currentGameplayScene, targetScene))
        {
            Debug.LogWarning($"Transition blocked: '{currentGameplayScene}' -> '{targetScene}'.");
            return;
        }

        StartCoroutine(TransitionRoutine(targetScene));
    }

    private System.Collections.IEnumerator TransitionRoutine(string targetScene)
    {
        isTransitioning = true;

        if (!string.IsNullOrEmpty(currentGameplayScene))
        {
            Scene currentScene = SceneManager.GetSceneByName(currentGameplayScene);
            if (currentScene.isLoaded)
            {
                AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(currentGameplayScene);
                if (unloadOp != null)
                {
                    yield return unloadOp;
                }
            }
        }

        Scene target = SceneManager.GetSceneByName(targetScene);
        if (!target.isLoaded)
        {
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
            if (loadOp != null)
            {
                yield return loadOp;
            }
        }

        Scene loadedTarget = SceneManager.GetSceneByName(targetScene);
        if (loadedTarget.IsValid() && loadedTarget.isLoaded)
        {
            SceneManager.SetActiveScene(loadedTarget);
        }

        currentGameplayScene = targetScene;
        isTransitioning = false;

        Debug.Log($"Entered scene '{targetScene}'.");
    }

    private bool CanTransition(string fromScene, string toScene)
    {
        if (string.IsNullOrEmpty(fromScene))
        {
            return true;
        }

        if (fromScene == villageSceneName)
        {
            return toScene == archerySceneName || toScene == swordplaySceneName || toScene == ridingSceneName;
        }

        if (fromScene == archerySceneName || fromScene == swordplaySceneName || fromScene == ridingSceneName)
        {
            return toScene == villageSceneName;
        }

        return false;
    }

    private bool IsManagedScene(string sceneName)
    {
        return sceneName == villageSceneName ||
               sceneName == archerySceneName ||
               sceneName == swordplaySceneName ||
               sceneName == ridingSceneName;
    }
}