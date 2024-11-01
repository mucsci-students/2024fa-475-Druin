using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
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
}
