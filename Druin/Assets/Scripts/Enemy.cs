using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Movement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();

        FindObjectOfType<GameManager>().AddEnemy(gameObject);
    }

    void Update()
    {
        UpdateAnimationDirection();
    }

    private void UpdateAnimationDirection()
    {
        Vector2 direction = movement.CurrentDirection;

        if (direction.magnitude > 0.1f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                    animator.SetInteger("Direction", 3); // Moving Right
                else
                    animator.SetInteger("Direction", 2); // Moving Left
            }
            else
            {
                if (direction.y > 0)
                    animator.SetInteger("Direction", 0); // Moving Up
                else
                    animator.SetInteger("Direction", 1); // Moving Down
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WorldManager wm = FindObjectOfType<WorldManager>();
            string activeWorld = wm.GetActiveWorld();

            if (wm.isTransitioning || activeWorld == "BattleScene") return;

            // TODO: Uncomment when hooking battle scene to the main world
             BattleManager bm = FindObjectOfType<BattleManager>(true);
            // // Start the battle

            // // set enemy of battle manager
            // *depending on the enemy, just pass an int 0-2 instead of the game object*
             if(gameObject.tag == "PumpkinLight"){
                bm.setEnemy(0, false);
             }else if(gameObject.tag == "PumpkinDark"){
                bm.setEnemy(0,true);
             }else if(gameObject.tag == "GhostLight"){
                bm.setEnemy(1,false);
             }else if(gameObject.tag == "GhostDark"){
                bm.setEnemy(1,true);
             }else if(gameObject.tag == "BatLight"){
                bm.setEnemy(2,false);
             }else if(gameObject.tag == "BatDark"){
                bm.setEnemy(2,true);
             }

            // // set world of battle manager

            wm.world_before_battle = activeWorld;
            Destroy(gameObject);

            wm.SwitchWorldFading("BattleScene");
        }
    }

    void OnDestroy()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RemoveEnemy(gameObject);
        }
    }
}
