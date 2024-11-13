using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

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

    public NPCConversation[] battleTexts;

    public int enemy;

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

    public void loadBattle(){
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
                            , GameObject.Find("PlayerRunF").GetComponent<NPCConversation>()}; // 10

        GameObject.Find("BattleManager").GetComponent<BattleManager>().setEnemy(enemy);
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