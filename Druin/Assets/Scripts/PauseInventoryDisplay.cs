using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

// Modified Menu3 for the UI without battle manager pieces
public class PauseInventoryDisplay : MonoBehaviour
{
    private GameObject[] itemTexts; // References to UI Text elements for each item
    private PlayerScript player;    // Reference to PlayerScript to access inventory

    void Start()
    {
        LoadInventoryUI();
        UpdateInventoryDisplay();
    }

    // This method sets up references to PlayerScript and item UI elements
    public void LoadInventoryUI()
    {
        player = GameObject.FindObjectOfType<PlayerScript>();
        
        itemTexts = new GameObject[]
        {
            GameObject.Find("HP Item Num"),
            GameObject.Find("FP Item Num"),
            GameObject.Find("Attack Item Num"),
            GameObject.Find("Defense Item Num"),
            GameObject.Find("Throwable Num")
        };
    }

    // This method updates the displayed quantities of each item type
    public void UpdateInventoryDisplay()
    {
        if (player == null) return;

        int[] nums = new int[5];
        for (int j = 0; j < player.itemAmount(); j++)
        {
            switch (player.getAccessToItem(j))
            {
                case itemAffect.HealthPotion:
                    nums[0]++;
                    break;
                case itemAffect.FPPotion:
                    nums[1]++;
                    break;
                case itemAffect.StatBoostAttack:
                    nums[2]++;
                    break;
                case itemAffect.StatBoostDefense:
                    nums[3]++;
                    break;
                case itemAffect.Throwable:
                    nums[4]++;
                    break;
            }
        }

        for (int i = 0; i < itemTexts.Length; i++)
        {
            itemTexts[i].GetComponent<Text>().text = nums[i].ToString();
        }

        GameObject.Find("Health Text").GetComponent<TMP_Text>().text = "Health: " + Convert.ToString(player.hp) + "/" + Convert.ToString(player.maxHP);
        GameObject.Find("Level").GetComponent<TMP_Text>().text = "Level: " + Convert.ToString(player.level);
    }

    // Method to use an HP item and reduce its count
    public void UseHPItem()
    {
        if (player == null) return;

        // Find the current amount of HP items
        int hpItemCount = 0;
        for (int j = 0; j < player.itemAmount(); j++)
        {
            if (player.getAccessToItem(j) == itemAffect.HealthPotion)
            {
                hpItemCount++;
            }
        }

        // Only use an item if there’s at least one HP item available
        if (hpItemCount > 0)
        {
            player.useItem(itemAffect.HealthPotion); 

            //Im just being lazy and copy pasting this here, I understand what went wrong,
            //my use item within the playerscript should have actually applied the item effect,
            //instead of just deleting the item from the inventory
            Item item = Items.itemInfo[0];
                if((player.maxHP) <= item.value || (item.value + player.hp) >= player.maxHP){
                    player.hp = player.maxHP;
                }else{
                    player.hp += item.value;
                }

            UpdateInventoryDisplay();
        }
        else
        {
            Debug.Log("No HP items available.");
        }
    }

    public void UseFPItem()
    {
        if (player == null) return;

        // Find the current amount of HP items
        int fpItemCount = 0;
        for (int j = 0; j < player.itemAmount(); j++)
        {
            if (player.getAccessToItem(j) == itemAffect.FPPotion)
            {
                fpItemCount++;
            }
        }

        // Only use an item if there’s at least one HP item available
        if (fpItemCount > 0)
        {
            player.useItem(itemAffect.FPPotion); 

            //Same with this one
            Item item = Items.itemInfo[1];
            if((player.maxFP) <= item.value || (item.value + player.fp) >= player.maxFP){
                    player.fp = player.maxFP;
                }else{
                    player.fp += item.value;
                }

            UpdateInventoryDisplay();
        }
        else
        {
            Debug.Log("No HP items available.");
        }
    }
}