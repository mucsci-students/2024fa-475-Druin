using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private int hp;

    private int fp;

    private int attack;

    private Attacks[] attacks;

    private int defense;

    private bool isDefending;

    private int level;

    private int exp;

    private int toNextLevel;

    private List<itemAffect> items;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;

        hp = 285 + (15 * level);
        fp = 18 + (2 * level);
        attack = 10 + (3 * level);
        defense = 10 + (2 * level);
        
        attacks = new[] {new Attacks("name1", 0, (5 * attack)), new Attacks("name2", 0, (10 * attack)), new Attacks("name3", 0, (12 * attack))};

        items = new List<itemAffect>();
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

    public void addItem(Item item){
        items.Add(item.affect);
    }

    public itemAffect getAccessToItem(int index){
        return items[index];
    }

    public int itemAmount(){
        return items.Count;
    }

    public bool useItem(itemAffect type){
        if(items.Contains(type)){
            items.Remove(type);
            return true;
        }else{
            return false;
        }
        
    }


    

}
public struct Attacks{
    public string name;
    public int fpCost;
    public int damage;
    public Attacks(string n, int fp, int d){
        name = n;
        fpCost = fp;
        damage = d;
    }
}