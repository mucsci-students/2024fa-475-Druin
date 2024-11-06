using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;            // Movement speed
    public float moveDuration = 30f;     // Duration to move in one direction
    private float moveTimer = 0f;       // Timer to track movement duration
    private Vector2 currentDirection;   // Current movement direction

    /// <summary>
    /// Move the gameobject by input
    /// </summary>
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector2(moveX, moveY));
    }

    /// <summary>
    /// Move the gameobject towards a destination
    /// </summary>
    public void MoveTowards(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    /// <summary>
    /// Randomly move the game object in one of four directions.
    /// </summary>
    public void MoveRandom()
    {
        // If the timer reaches zero, pick a new direction
        if (moveTimer <= 0f)
        {
            ResetRandomDirection();
        }

        // Move in the chosen direction
        //Vector2 targetPosition = (Vector2)transform.position + currentDirection * speed * Time.deltaTime;
        //transform.position = targetPosition;
        transform.Translate(currentDirection * speed * Time.deltaTime);

        // Decrement the move timer
        moveTimer -= Time.deltaTime;

        // Check for obstacles, and pick a new direction if there's one ahead
        if (IsObstacleAhead())
        {
            ResetRandomDirection();
        }
    }

    /// <summary>
    /// Picks a random direction and resets the move timer
    /// </summary>
    private void ResetRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            case 0: currentDirection = Vector2.up;    break;
            case 1: currentDirection = Vector2.down;  break;
            case 2: currentDirection = Vector2.left;  break;
            case 3: currentDirection = Vector2.right; break;
        }

        // Reset the timer
        moveTimer = moveDuration;
    }

    /// <summary>
    /// Checks if there's an obstacle in the current direction
    /// </summary>
    private bool IsObstacleAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, 0.5f);

        // Ignore the player object as an obstacle
        // TODO: Make some "boarder" gameobjects to stop enemy, and change the name here to them.
        // i.e. return hit.collider != null && hit.collider.CompareTag("Boarder");
        return hit.collider != null && false;
    }
}
