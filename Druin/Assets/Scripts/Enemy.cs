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

            // TODO: for testing only
            string activeWorld = wm.GetActiveWorld();
            if (wm.isTransitioning || activeWorld == "BattleScene")
            {
                // Already in a battle with another enemy
                return;
            }
            wm.world_before_battle = activeWorld;
            Destroy(gameObject);

            // TODO: Uncomment when hooking battle scene to the main world
            // BattleManager bm = FindObjectOfType<BattleManager>();
            // // Start the battle

            // // set enemy of battle manager
            // bm.setEnemy(gameObject);

            // // set world of battle manager
            // bm.world = wm.GetActiveWorld();

            wm.SwitchWorldFading("BattleScene");
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
