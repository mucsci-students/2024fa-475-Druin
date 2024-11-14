using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Enemy2 : EnemyStats
{
    public override void Start(){
        enemyName = "Enemy2";
        HP = 200;
        Attack = 50;
        Defense = 3;
        EXP = 15;
        isBoss = false;


        attacks = new[] {new Attacks("attack1", 0, 8), new Attacks("attack2", 0, 16), new Attacks("attack3", 1, 22)};

        battleTexts = new[] {GameObject.Find("Enemy2Attack1").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy2Attack2").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy2RunSuccess").GetComponent<NPCConversation>()
                            , GameObject.Find("Enemy2RunFail").GetComponent<NPCConversation>()};
    }

    public override void resetStats(){
        HP = 200;
    }
}