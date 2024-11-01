using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private int hp;

    private int fp;

    private int attack;

    private int defense;

    private bool isDefending;

    private int level;

    private int exp;

    private int toNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;

        hp = 285 + (15 * level);
        fp = 18 + (2 * level);
        attack = 10 + (3 * level);
        defense = 10 + (2 * level);
    }

    public int getHP(){
        return hp;
    }

    public void setHP(int val){
        hp = val;
    }

    public int getFP(){
        return fp;
    }

    public void setFP(int val){
        fp = val;
    }

    public int getExp(){
        return exp;
    }

    public void addExp(int val){
        exp += val;
    }

    public int getLevel(){
        return level;
    }

    public void setDefending(){
        if(!isDefending){
            isDefending = true;
            defense = defense * 2;
        }
    }

    public void stopDefending(){
        if(isDefending){
            isDefending = false;
            defense = defense / 2;
        }
    }

}
