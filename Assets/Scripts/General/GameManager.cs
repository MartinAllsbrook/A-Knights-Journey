using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTag
{
    Village,
    Swordplay,
    Archery,
    Riding,
    Tournament
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<SceneTag, string> sceneMap = new Dictionary<SceneTag, string>
    {
        { SceneTag.Archery, "ArcheryGame" },
        { SceneTag.Swordplay, "SwordplayGame" },
        { SceneTag.Riding, "RidingGame" },
        { SceneTag.Village, "Village" },
        { SceneTag.Tournament, "Tournament" }
    };

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
            EnterScene(SceneTag.Village);
        }
    }

    public string CurrentGameplayScene => currentGameplayScene;

    public void SetInitialGameplayScene(string sceneName)
    {
        currentGameplayScene = sceneName;
    }

    public void EnterScene(SceneTag key)
    {
        if (isTransitioning || !sceneMap.ContainsKey(key)) return;
        SwitchScene(sceneMap[key]);
    }

    public void SwitchScene(string sceneName)
    {
        isTransitioning = true;
        currentGameplayScene = sceneName;
        SceneManager.LoadScene(sceneName);
        isTransitioning = false;
    }
}
