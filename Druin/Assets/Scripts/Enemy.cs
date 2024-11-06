using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            WorldManager wm = FindObjectOfType<WorldManager>();
            // TODO: Uncomment when hooking battle scene to the main world
            // BattleManager bm = FindObjectOfType<BattleManager>();
            // // Start the battle

            // // set enemy of battle manager
            // bm.setEnemy(gameObject);

            // // set world of battle manager
            // bm.world = wm.GetActiveWorld();

            // TODO: for testing only
            wm.world_before_battle = wm.GetActiveWorld();
            Destroy(gameObject);

            FindObjectOfType<GameManager>().playerControlled = false;
            wm.SwitchToWorld("BattleScene");
        }
    }

    void OnDestroy()
    {
        // Remove this enemy from the GameManager's list
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RemoveEnemy(gameObject);
        }
    }
}
