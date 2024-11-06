using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Items : object
{
    
    //This variable is meant to hold all the kinds of items we will have
    //the only interaction with this info is from getItemInfo
    private static Item[] itemInfo= new[] {new Item("HP Restore", itemAffect.HealthPotion, 10),
                            new Item("FP Restore", itemAffect.FPPotion, 10),
                            new Item("Attack Boost", itemAffect.StatBoostAttack, 10),
                            new Item("Defense Boost", itemAffect.StatBoostDefense, 10),
                            new Item("Throwing Knife", itemAffect.Throwable, 10)};

    public static string getItemName(int index){
        return itemInfo[index].name;
    }

    public static int getItemVal(int index){
        return itemInfo[index].value;
    }

    
}
public enum itemAffect{
        HealthPotion,
        FPPotion,
        StatBoostAttack,
        StatBoostDefense,
        Throwable
    }

    public struct Item{
        public string name;

        //Affect is an array to allow a specific effect, with the value stored in 1 spot in an array
        //Index 0 = Health potion effect
        //Index 1 = FP potion effect
        //Index 2 = Stat boost effect
        //Index 3 = 
        public itemAffect affect;

        public int value;

        public Item(string n, itemAffect a, int v){
            name = n;

            affect = a;

            value = v;
        }
    }