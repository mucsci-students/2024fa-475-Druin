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
            ConversationManager.Instance.StartConversation(conversation);
            Destroy(gameObject);
        }
    }
}
