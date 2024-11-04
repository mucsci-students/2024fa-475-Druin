using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        StartCoroutine(LoadScenes());
    }

    void Update()
    {
        // Press Tab to switch worlds
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsWorldActive("World_Light"))
            {
                SwitchToWorld("World_Dark");
            }
            else if (IsWorldActive("World_Dark"))
            {
                SwitchToWorld("BattleScene");
            }
            else
            {
                SwitchToWorld("World_Light");
            }
        }
    }

    private IEnumerator LoadScenes()
    {
        // Load World_Light first
        AsyncOperation loadLight = SceneManager.LoadSceneAsync("World_Light", LoadSceneMode.Additive);
        yield return loadLight;

        // Load World_Dark and keep it inactive at first
        AsyncOperation loadDark = SceneManager.LoadSceneAsync("World_Dark", LoadSceneMode.Additive);
        yield return loadDark;

        // Load BattleScene and keep it inactive at first
        AsyncOperation loadBattle = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
        yield return loadBattle;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("World_Light"));

        // Set initial active states
        SetWorldActive("World_Light", true);
        SetWorldActive("World_Dark", false);
        SetWorldActive("BattleScene", false);
    }

    public void SwitchToWorld(string sceneName)
    {
        bool isWorldActive = IsWorldActive(sceneName);
        
        // Already active
        if (isWorldActive)
        {
            return;
        }

        // Toggle the active states
        if (sceneName == "World_Light")
        {
            SetWorldActive("World_Light", !isWorldActive);
            SetWorldActive("World_Dark", isWorldActive);
            SetWorldActive("BattleScene", isWorldActive);
        }
        else if (sceneName == "World_Dark")
        {
            SetWorldActive("World_Light", isWorldActive);
            SetWorldActive("World_Dark", !isWorldActive);
            SetWorldActive("BattleScene", isWorldActive);
        }
        else
        {
            SetWorldActive("World_Light", isWorldActive);
            SetWorldActive("World_Dark", isWorldActive);
            SetWorldActive("BattleScene", !isWorldActive);
        }
    }

    private bool IsWorldActive(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].activeSelf;
    }

    private void SetWorldActive(string sceneName, bool isActive)
    {
        if (isActive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        foreach (GameObject go in SceneManager.GetSceneByName(sceneName).GetRootGameObjects())
        {
            go.SetActive(isActive);
        }
    }
}
