using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public abstract class EnemyStats : MonoBehaviour
{
    public string enemyName;

    public int HP;

    public int Attack;

    public int Defense;

    public int EXP;

    public bool isDark;

    public bool isBoss;

    public bool windUpAttack;

    public int windUpTurn;

    public Attacks[] attacks;

    public NPCConversation[] battleTexts;

    public abstract void Start();

    public abstract void resetStats();
}