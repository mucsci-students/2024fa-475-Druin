using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Boss : EnemyStats
{
    public override void Start(){
        enemyName = "Boss";
        HP = 2000;
        Attack = 50;
        Defense = 20;
        EXP = 15;
        isBoss = true;

        attacks = new[] {new Attacks("Palm strike", 0, 20), new Attacks("Antler stab", 0, 35), new Attacks("Heal",0, 80),new Attacks("Absolute Darkness", 1, 300)};

        battleTexts = new[] {GameObject.Find("BossAttack1").GetComponent<NPCConversation>()
                            , GameObject.Find("BossAttack2").GetComponent<NPCConversation>()
                            , GameObject.Find("BossHeal").GetComponent<NPCConversation>()
                            , GameObject.Find("BossAttack3").GetComponent<NPCConversation>()
                            , GameObject.Find("BossWindup").GetComponent<NPCConversation>()};
    }

    public override void resetStats(){
        HP = 3000;
    }
}
