using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the player gameobject
    /// </summary>
    public GameObject player;
    
    /// <summary>
    /// References to enemy gameobjects
    /// </summary>
    public List<GameObject> enemies;

    /// <summary>
    /// Whether or not the player can control movement
    /// </summary>
    public bool playerControlled = true;

    public WorldManager worldManager;

    public bool puzzleSolved = false;

    void Start()
    {
        worldManager = FindObjectOfType<WorldManager>();
    }

    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive)
        {
            return;
        }
        if (worldManager.isTransitioning || worldManager.IsWorldActive("BattleScene"))
        {
            return;
        }

        // Player movement
        if (playerControlled)
        {
            player.GetComponent<Movement>().Move();
        }
        else
        {
            // TODO: Move towards a destination or don't move
        }

        // Enemy movement
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                enemy.GetComponent<Movement>().MoveRandom();
            }
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
