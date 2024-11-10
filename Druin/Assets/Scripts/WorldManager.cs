using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    // TODO: for testing only
    public string world_before_battle;

    public Image whiteOverlay; // Assign this with a fullscreen white UI image

    public bool isTransitioning = false;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        StartCoroutine(LoadScenes());
    }

    void Start()
    {
        whiteOverlay = GameObject.Find("WhiteOverlay").GetComponent<Image>();
    }

    void Update()
    {
        if (isTransitioning)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsWorldActive("World_Light"))
            {
                SwitchWorldFading("World_Dark");
            }
            else if (IsWorldActive("World_Dark"))
            {
                SwitchWorldFading("World_Light");
            }
        }
        else if (IsWorldActive("BattleScene"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SwitchWorldFading(world_before_battle);
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

        // Set initial active states
        SetWorldActive("World_Light", true);
        SetWorldActive("World_Dark", false);
        SetWorldActive("BattleScene", false);
    }

    public void SwitchWorldFading(string sceneName)
    {
        StartCoroutine(SwitchWorldsWithTransition(sceneName));
    }

    public IEnumerator SwitchWorldsWithTransition(string sceneName)
    {
        isTransitioning = true;

        float transitionDuration = 1f; // the time for fading in/out
        float elapsed = 0f;

        // Step 1: Fade to white over the transition duration
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / transitionDuration); // Interpolate from 0 to 1
            whiteOverlay.color = new Color(1f, 1f, 1f, alpha); // Increase whiteness
            yield return null;
        }

        // Ensure it's fully white before switching
        whiteOverlay.color = new Color(1f, 1f, 1f, 1f);

        // Switch world
        SwitchToWorld(sceneName);

        // Step 2: Fade back to clear
        elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / transitionDuration)); // Interpolate from 1 to 0
            whiteOverlay.color = new Color(1f, 1f, 1f, alpha); // Decrease whiteness
            yield return null;
        }

        // Ensure overlay is fully transparent at the end
        whiteOverlay.color = new Color(1f, 1f, 1f, 0f);
        isTransitioning = false;
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

    public string GetActiveWorld()
    {
        if (IsWorldActive("World_Light"))
        {
            return "World_Light";
        }
        else if (IsWorldActive("World_Dark"))
        {
            return "World_Dark";
        }
        else
        {
            return "BattleScene";
        }
    }

    public bool IsWorldActive(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].activeSelf;
    }

    public void SetWorldActive(string sceneName, bool isActive)
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
