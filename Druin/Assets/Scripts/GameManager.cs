using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
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
            // For now, just chase the player
            enemy.GetComponent<Movement>().MoveTowards(player.transform.position);
        }
    }
}
