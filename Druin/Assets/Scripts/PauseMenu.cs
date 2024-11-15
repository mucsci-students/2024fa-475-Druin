using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu panel
    private bool isPaused = false; // Keeps track of pause state
    private GameObject dialoguePanel;
    public GameObject settingsBackground;
    public GameObject quitPrompt;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "BattleScene"
        && Input.GetKeyDown(KeyCode.Escape)
        && (dialoguePanel == null || !dialoguePanel.activeSelf) // Detect ESC key press
        && (dialoguePanel == null || !dialoguePanel.activeSelf)
        && !settingsBackground.activeSelf
        && !quitPrompt.activeSelf) // Detect ESC key press
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    public void PauseGame()
    {
        Debug.Log("Pausing game");
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;         
        pauseMenuUI.SetActive(true);    
        Time.timeScale = 0.0001f;
        isPaused = true;
        FindObjectOfType<PauseInventoryDisplay>().UpdateInventoryDisplay();
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;          
        isPaused = false;
    }
}
