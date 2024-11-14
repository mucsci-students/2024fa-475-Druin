using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;            // Movement speed
    public float moveDuration = 30f;    // Duration to move in one direction
    private float moveTimer = 0f;       // Timer to track movement duration
    private Vector2 currentDirection;   // Current movement direction

    public LayerMask borderLayerMask; // Expose this field in the Inspector

    public Vector2 CurrentDirection => currentDirection;
    private bool facingRight = true;
    private Vector2 lastMoveDirection;
    private Animator animator;

    void Start()
    {
        borderLayerMask = LayerMask.GetMask("Border");
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 moveVector = new Vector2(moveX, moveY);

        // Update direction if there's movement
        if (moveVector != Vector2.zero)
        {
            lastMoveDirection = moveVector.normalized;
            currentDirection = moveVector.normalized;

            // Set animation based on movement direction
            if (moveY < 0) // Moving down
            {
                animator.Play("PlayerForwardWalk");
            }
            else if (moveY > 0) // Moving up
            {
                animator.Play("PlayerBackWalk");
            }
            else if (moveX != 0) // Moving left or right
            {
                animator.Play("PlayerSideWalk");
                FlipSprite(moveX); // Flip the sprite based on left/right direction
            }
        }
        else
        {
            SetIdleAnimation();
        }

        transform.Translate(moveVector);
    }

    private void SetIdleAnimation()
    {
        // Check the last movement direction and set the appropriate idle animation
        if (lastMoveDirection.y < 0) // Last movement was down
        {
            animator.Play("ForwardIdle");
        }
        else if (lastMoveDirection.y > 0) // Last movement was up
        {
            animator.Play("Idleback");
        }
        else // Last movement was left or right
        {
            animator.Play("SideIdle");
        }
    }

    private void FlipSprite(float moveX)
    {
        // Check if facing direction needs to change
        if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Flip the x scale to mirror the sprite
            transform.localScale = scale;
        }
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
