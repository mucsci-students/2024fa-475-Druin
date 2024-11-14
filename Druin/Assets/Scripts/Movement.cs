using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;            // Movement speed
    public float moveDuration = 30f;    // Duration to move in one direction
    private float moveTimer = 0f;       // Timer to track movement duration
    private Vector2 currentDirection;   // Current movement direction

    public LayerMask borderLayerMask; // Expose this field in the Inspector

    public Vector2 CurrentDirection => currentDirection;

    void Start()
    {
        borderLayerMask = LayerMask.GetMask("Border");
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 moveVector = new Vector2(moveX, moveY);

        // Update direction if there's movement
        if (moveVector != Vector2.zero)
        {
            currentDirection = moveVector.normalized;
        }

        transform.Translate(moveVector);
    }

    public void MoveTowards(Vector2 destination)
    {
        Vector2 direction = destination - (Vector2)transform.position;
        if (direction.magnitude > 0.1f)
        {
            currentDirection = direction.normalized;
        }
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    public void MoveRandom()
    {
        if (moveTimer <= 0f)
        {
            ResetRandomDirection();
            return;
        }

        // Check if there is an obstacle ahead first
        if (IsObstacleAhead())
        {
            ResetRandomDirection();
            return; // Stop moving if an obstacle is ahead
        }

        // Move the monster if no obstacle detected
        transform.Translate(currentDirection * speed * Time.deltaTime);
        moveTimer -= Time.deltaTime;
    }


    private void ResetRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            case 0: currentDirection = Vector2.up; break;
            case 1: currentDirection = Vector2.down; break;
            case 2: currentDirection = Vector2.left; break;
            case 3: currentDirection = Vector2.right; break;
        }

        moveTimer = moveDuration;
    }


    private bool IsObstacleAhead()
    {
        // Raycast using both tag filtering and layer mask to check for the Border layer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, 1.0f, borderLayerMask);

        // Ensure the hit object is tagged as "Border"
        return hit.collider != null && hit.collider.CompareTag("Border");
    }

}
