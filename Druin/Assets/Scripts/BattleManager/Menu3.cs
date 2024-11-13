using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menu3 : Menu
{
    //These will be the numbers that indicate how much of each number the player still has
    private GameObject[] itemTexts;

    //Required to access the players items
    private PlayerScript player;
    public bool isLoaded = false;
    
    public void loading(){
        player = GameObject.FindObjectOfType<PlayerScript>();
        
        itemTexts = new GameObject[] {GameObject.Find("HP Item Num"),
                                        GameObject.Find("FP Item Num"),
                                        GameObject.Find("Attack Item Num"),
                                        GameObject.Find("Defense Item Num"),
                                        GameObject.Find("Throwable Num")};
    }
        
    public void updateNums(){
        int[] nums = new int[5];
        for(int j = 0; j < player.itemAmount(); j++){
            if(player.getAccessToItem(j) == itemAffect.HealthPotion){
                nums[0] += 1;
            }
            if(player.getAccessToItem(j) == itemAffect.FPPotion){
                nums[1] += 1;
            }
            if(player.getAccessToItem(j) == itemAffect.StatBoostAttack){
                nums[2] += 1;
            }
            if(player.getAccessToItem(j) == itemAffect.StatBoostDefense){
                nums[3] += 1;
            }
            if(player.getAccessToItem(j) == itemAffect.Throwable){
                nums[4] += 1;
            }
        }

        for(int i = 0; i < itemTexts.Length; i++){
            itemTexts[i].GetComponent<Text>().text = Convert.ToString(nums[i]);
        }
    }
        

        //set up an items array list in playerscript then come back here
    
    public override void handleInteraction(int pos){
        if(pos == 4){
            battleManager.useItem(0);
        }
        if(pos == 5){
            battleManager.useItem(1);
        }
        if(pos == 6){
            battleManager.useItem(2);
        }
        if(pos == 7){
            battleManager.useItem(3);
        }
        if(pos == 8){
            battleManager.useItem(4);
        }
        if(pos == 9){
            battleManager.changeMenu(0);
        }
    }
}
