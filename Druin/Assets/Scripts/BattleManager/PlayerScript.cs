using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PlayerScript : MonoBehaviour
{

    public int hp;

    public int maxHP;

    public int fp;

    public int maxFP;

    public int attack;

    public Attacks[] attacks;

    public int defense;

    private bool isDefending;

    public int level;

    public int exp;

    public int toNextLevel;

    private List<itemAffect> items;

    public NPCConversation[] battleTexts;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;

        maxHP = 85 + (15 * level);
        hp = maxHP;
        maxFP = 18 + (2 * level);
        fp = maxFP;
        attack = 10 + (3 * level);
        defense = 5 + (2 * level);

        exp = 0;
        toNextLevel = 10 + (12 * level);
        
        attacks = new[] {new Attacks("Punch", 0, (attack)), new Attacks("Knife Attack", 2, (2 * attack)), new Attacks("Bow Shot", 5, (5 * attack))};

        items = new List<itemAffect>();

        
    }

    public void getEXP(int val){
        exp += val;
        if(exp >= toNextLevel){
            exp -= toNextLevel;
            levelUP();
        }
    }

    private void levelUP(){
        level++;

        maxHP = 85 + (15 * level);
        hp = maxHP;
        maxFP = 18 + (2 * level);
        fp = maxFP;
        attack = 10 + (3 * level);
        defense = 5 + (2 * level);
        toNextLevel = 10 + (12 * level);

        attacks[0].damage = attack;
        attacks[1].damage = attack * 2;
        attacks[2].damage = attack * 5;
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

    public void boostAttack(){
        attack += (3 + (int)(1.5 * level));
        attacks[0].damage = attack;
        attacks[1].damage = attack * 2;
        attacks[2].damage = attack * 5;
    }

    public void boostDefense(){
        defense += (3 + (int)(1.5 * level));
    }

    public void loadBattleTexts(){
        battleTexts = new[] {GameObject.Find("PlayerAttack1").GetComponent<NPCConversation>() //index 0
                            , GameObject.Find("PlayerAttack2").GetComponent<NPCConversation>() // 1
                            , GameObject.Find("PlayerAttack3").GetComponent<NPCConversation>() // 2
                            , GameObject.Find("PlayerDefends").GetComponent<NPCConversation>() // 3
                            , GameObject.Find("PlayerHPRestore").GetComponent<NPCConversation>() // 4
                            , GameObject.Find("PlayerFPRestore").GetComponent<NPCConversation>() // 5
                            , GameObject.Find("PlayerAttackBoost").GetComponent<NPCConversation>() // 6
                            , GameObject.Find("PlayerDefenseBoost").GetComponent<NPCConversation>() // 7
                            , GameObject.Find("PlayerUseThrowable").GetComponent<NPCConversation>() // 8
                            , GameObject.Find("PlayerRunS").GetComponent<NPCConversation>() // 9
                            , GameObject.Find("PlayerRunF").GetComponent<NPCConversation>()// 10
                            , GameObject.Find("PlayerNoFP").GetComponent<NPCConversation>()//11
                            , GameObject.Find("PlayerNoItem").GetComponent<NPCConversation>()}; //12

        
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