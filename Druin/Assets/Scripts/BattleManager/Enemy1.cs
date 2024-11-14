using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Enemy1 : EnemyStats
{
    public override void Start(){
        enemyName = "Enemy1";
        HP = 100;
        Attack = 50;
        Defense = 5;
        EXP = 15;

        attacks = new[] {new Attacks("attack1", 0, 10), new Attacks("attack2", 0, 12), new Attacks("attack3", 1, 18)};

        battleTexts = new[] {GameObject.Find("Enemy1Attack1").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy1Attack2").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy1RunSuccess").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy1RunFail").GetComponent<NPCConversation>()};
    }

    public override void resetStats(){
        HP = 100;
    }
}