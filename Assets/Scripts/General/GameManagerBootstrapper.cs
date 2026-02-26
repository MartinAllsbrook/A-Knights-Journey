#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManagerBootstrapper
{
    private const string GameManagerPrefabPath = "Assets/Prefabs/GameManager.prefab";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureGameManagerForDirectSceneTesting()
    {
        if (GameManager.Instance != null)
        {
            return;
        }

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(GameManagerPrefabPath);
        GameManager manager;

        if (prefab != null)
        {
            GameObject instance = Object.Instantiate(prefab);
            manager = instance.GetComponent<GameManager>();
        }
        else
        {
            GameObject fallbackObject = new GameObject(nameof(GameManager));
            manager = fallbackObject.AddComponent<GameManager>();
            Debug.LogWarning($"Could not load GameManager prefab at '{GameManagerPrefabPath}'. Created fallback GameManager for editor play mode testing.");
        }

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.IsValid() && activeScene.isLoaded && !string.IsNullOrWhiteSpace(activeScene.name))
        {
            manager.SetInitialGameplayScene(activeScene.name);
        }
    }
}
#endif
