using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update

    private Item[] itemInfo;
    void Start()
    {
        //Need to fill in the rest of the values, also will need to playtest each item to make sure they balanced
        itemInfo = new[] {new Item("HP restore", itemAffect.HealthPotion, 10)};
    }

    public enum itemAffect{
        HealthPotion,
        FPPotion,
        StatBoost,
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
}
