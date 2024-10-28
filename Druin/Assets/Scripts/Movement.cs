using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /// <summary>
    /// Movement speed
    /// </summary>
    public float speed;

    /// <summary>
    /// Move the gameobject by input
    /// </summary>
    public void Move()
    {
        // WASD or Arrow keys
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical")   * speed * Time.deltaTime;

        // Directly move the gameobject by updating the transform position
        transform.Translate(new Vector2(moveX, moveY));
    }

    /// <summary>
    /// Move the gameobject towards destination
    /// </summary>
    /// <param name="destination"> The destination </param>
    public void MoveTowards(Vector2 destination)
    {
        // Update position using Vector2.MoveTowards
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
