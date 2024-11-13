using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : MonoBehaviour
{
    public string enemyName;

    public int HP;

    public int Attack;

    public int Defense;

    public int EXP;

    public Attacks[] attacks;

    public abstract void Start();
}