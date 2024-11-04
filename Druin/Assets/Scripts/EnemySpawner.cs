using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Assign your enemy prefab in the Inspector
    public float spawnInterval = 1f; // Time interval between spawns
    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        // Start the spawning process
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        // Instantiate the enemy at the spawner's position with no rotation
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Add the spawned enemy to the GameManager's list
        if (gameManager != null)
        {
            gameManager.AddEnemy(enemy);
        }
    }
}
