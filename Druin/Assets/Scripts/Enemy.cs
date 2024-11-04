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
            // Start the battle
            Debug.Log($"Battle begins! with {gameObject.name}");
            GameObject.FindObjectOfType<BattleManager>().setEnemy(gameObject);
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
