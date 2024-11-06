using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldSwitchCountdown : MonoBehaviour
{
    private float countdownDuration = 10f;
    private float countdownTimer = 10f;   // Set an initial countdown timer

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the countdown text is set initially
        UpdateCountdownText();
    }

    // Update is called once per frame
    void Update()
    {
        WorldManager wm = FindObjectOfType<WorldManager>();
        if (wm.GetActiveWorld() == "BattleScene")
        {
            return;
        }

        // Decrease the timer each frame
        countdownTimer -= Time.deltaTime;

        // Update the displayed text
        UpdateCountdownText();

        // Reset or handle timer when it reaches zero
        if (countdownTimer <= 0)
        {
            countdownTimer = countdownDuration; // Reset timer
            
            // handle scene switching logic
            if (wm.GetActiveWorld() == "World_Light")
            {
                wm.SwitchToWorld("World_Dark");
            }
            else
            {
                wm.SwitchToWorld("World_Light");
            }
        }
    }

    private void UpdateCountdownText()
    {
        // Display the countdown timer rounded to one decimal place
        GetComponent<TextMeshProUGUI>().text = $"World Switching in: {countdownTimer:F1} seconds";
    }
}
