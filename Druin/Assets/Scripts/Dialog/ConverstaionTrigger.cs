using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConverstaionTrigger : MonoBehaviour
{
    public NPCConversation conversation;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (name == "SolvedPuzzle")
            {
                FindObjectOfType<GameManager>().puzzleSolved = true;
            }
            ConversationManager.Instance.StartConversation(conversation);
            if (FindObjectOfType<GameManager>().puzzleSolved)
            {
                // MDF: May print warning, doesn't matter
                ConversationManager.Instance.SetBool("PuzzleSolved", true);
            }
            Destroy(gameObject);
        }
    }
}
