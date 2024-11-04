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
            SwitchWorlds();
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

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("World_Light"));

        // Set initial active states
        SetWorldActive("World_Light", true);
        SetWorldActive("World_Dark", false);
    }

    public void SwitchWorlds()
    {
        bool isWorldLightActive = IsWorldActive("World_Light");

        // Toggle the active states
        SetWorldActive("World_Light", !isWorldLightActive);
        SetWorldActive("World_Dark", isWorldLightActive);
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
