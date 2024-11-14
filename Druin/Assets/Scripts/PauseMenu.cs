using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu panel
    private bool isPaused = false; // Keeps track of pause state
    private GameObject dialoguePanel;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "BattleScene"
        && Input.GetKeyDown(KeyCode.Escape)
        && (dialoguePanel == null || !dialoguePanel.activeSelf)) // Detect ESC key press
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

    public void PauseGame()
    {
        Debug.Log("Pausing game");
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;         
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;          
        isPaused = false;
    }
}
