using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy1 : EnemyStats
{
    public override void Start(){
        enemyName = "Enemy1";
        HP = 100;
        Attack = 50;
        Defense = 25;
        EXP = 15;

        attacks = new[] {new Attacks("attack1", 0, 10), new Attacks("attack2", 0, 12), new Attacks("attack3", 1, 18)};
    }
}