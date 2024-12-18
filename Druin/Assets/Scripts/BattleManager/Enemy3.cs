using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Enemy3 : EnemyStats
{
    public override void Start(){
        enemyName = "Enemy3";
        HP = 75;
        Attack = 50;
        Defense = 2;
        EXP = 15;
        isBoss = false;

        attacks = new[] {new Attacks("attack1", 0, 12), new Attacks("attack2", 0, 25), new Attacks("attack3", 1, 40)};

        battleTexts = new[] {GameObject.Find("Enemy3Attack1").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy3Attack2").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy3RunSuccess").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy3RunFail").GetComponent<NPCConversation>()};
    }

    public override void resetStats(){
        HP = 75;
    }
}