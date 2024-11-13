using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DialogueEditor;

public class BattleManager : MonoBehaviour
{
    private int turnCount;

    private bool enemysTurn;
    private GameObject player;
    private PlayerScript playerScript;

    private EnemyStats enemy;

    private GameObject[] menuCanvi;

    private GameObject currentCanvas;

    private Menu[] menuScripts;

    private Vector3[] cursorSpaces;

    private GameObject cursor;

    
    // Start is called before the first frame update
    
    void Start()
    {
        turnCount = 1;
        enemysTurn = false;

        //used to get the position for the cursor to be at for the menu
        //invisible game objects are placed where the cursor will be when looking at a specific option
        cursorSpaces = new[] {getSpotPos("TriSpot1"), getSpotPos("TriSpot2"), getSpotPos("TriSpot3"), getSpotPos("TriSpot4"),
                                getSpotPos("ItemSpot1"), getSpotPos("ItemSpot2"), getSpotPos("ItemSpot3"), getSpotPos("ItemSpot4"),
                                getSpotPos("ItemSpot5"), getSpotPos("ItemSpot6")};
        

        menuCanvi = new[] {getCanvas("Menu1"), getCanvas("Menu2"), getCanvas("Menu3")};
        currentCanvas = menuCanvi[0];

        menuScripts = new Menu[] {GameObject.FindObjectOfType<Menu1>(), GameObject.FindObjectOfType<Menu2>(), GameObject.FindObjectOfType<Menu3>()};

        //this code finds the cursor game object and ensures that it is at the start position
        cursor = GameObject.FindWithTag("Cursor");
        setCursorPos(0);

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerScript.loadBattle();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(ConversationManager.Instance != null && !ConversationManager.Instance.IsConversationActive){
            //pressing the Up arrow key will send the cursor to the top row no matter what column it is in
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                if(getCursorY() == -4.21f){
                    if(getCursorX() == -3f){
                        setCursorPos(0);
                    }else{
                        setCursorPos(2);
                    }
                }
                if(getCursorY() == 2f){
                    setCursorPos(4);
                }
                if(getCursorY() == 1.05f){
                    setCursorPos(5);
                }
                if(getCursorY() == .1f){
                    setCursorPos(6);
                }
                if(getCursorY() == -.88f){
                    setCursorPos(7);
                }
                if(getCursorY() == -2f){
                    setCursorPos(8);
                }
                
            }

            //pressing the Down arrow key will send the cursor to the bottom row no matter what column it is in
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                if(getCursorY() == -2.84f){
                    if(getCursorX() == -3f){
                        setCursorPos(1);
                    }else{
                        setCursorPos(3);
                    }
                }
                if(getCursorY() == -.88f){
                    setCursorPos(9);
                }
                if(getCursorY() == .1f){
                    setCursorPos(8);
                }
                if(getCursorY() == 1.05f){
                    setCursorPos(7);
                }
                if(getCursorY() == 2f){
                    setCursorPos(6);
                }
                if (getCursorY() == 2.95f){
                    setCursorPos(5);
                }
                
            }

            //pressing the Right arrow key in the 1st column will send it to the 2nd column no matter the row
            if(Input.GetKeyDown(KeyCode.RightArrow) && (getCursorX() == -3f)){
                if(getCursorY() == -2.84f){
                    setCursorPos(2);
                }else{
                    setCursorPos(3);
                }
            }

            //pressing the Left arrow key in the 1st column will send it to the 2nd column no matter the row
            if(Input.GetKeyDown(KeyCode.LeftArrow) && (getCursorX() == 0.8f)){
                if(getCursorY() == -2.84f){
                    setCursorPos(0);
                }else{
                    setCursorPos(1);
                }
            }

            //interacts with the option at the cursors position
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
                if(currentCanvas.name == "Menu1"){
                    if(checkCursorPos(cursorSpaces[0])){
                        menuScripts[0].handleInteraction(0);
                    }else if(checkCursorPos(cursorSpaces[1])){
                        menuScripts[0].handleInteraction(1);
                    }else if(checkCursorPos(cursorSpaces[2])){
                        menuScripts[0].handleInteraction(2);
                    }else{
                        menuScripts[0].handleInteraction(3);
                    }
                }
                else if(currentCanvas.name == "Menu2"){
                    if(checkCursorPos(cursorSpaces[0])){
                        menuScripts[1].handleInteraction(0);
                    }else if(checkCursorPos(cursorSpaces[1])){
                        menuScripts[1].handleInteraction(1);
                    }else if(checkCursorPos(cursorSpaces[2])){
                        menuScripts[1].handleInteraction(2);
                    }else{
                        menuScripts[1].handleInteraction(3);
                    }
                } 
                else if(currentCanvas.name == "Menu3"){
                    if(checkCursorPos(cursorSpaces[4])){
                        menuScripts[2].handleInteraction(4);
                    }else if(checkCursorPos(cursorSpaces[5])){
                        menuScripts[2].handleInteraction(5);
                    }else if(checkCursorPos(cursorSpaces[6])){
                        menuScripts[2].handleInteraction(6);
                    }else if(checkCursorPos(cursorSpaces[7])){
                        menuScripts[2].handleInteraction(7);
                    }else if(checkCursorPos(cursorSpaces[8])){
                        menuScripts[2].handleInteraction(8);
                    }else{
                        menuScripts[2].handleInteraction(9);
                    }
                }
            }
        }else{
                
                    if(!enemysTurn){
                        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
                        ConversationManager.Instance.PressSelectedOption();
                        enemyDecisions();
                        }
                    }else{
                        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
                        enemysTurn = false;
                        ConversationManager.Instance.PressSelectedOption();
                        }
                    }
            
        }
    }

    //changes the menu shown when required
    public void changeMenu(int index){
        //turns of the current menu
        
        if(currentCanvas.name == "Menu3"){
            GameObject.Find("RectangleBox").GetComponent<SpriteRenderer>().enabled = false;
        }
        currentCanvas.GetComponent<Canvas>().enabled = false;

        //switches to the next menu and activates it
        currentCanvas = menuCanvi[index];
        currentCanvas.GetComponent<Canvas>().enabled = true;
        setCursorPos(0);
        if(currentCanvas.name == "Menu3"){
            GameObject.Find("RectangleBox").GetComponent<SpriteRenderer>().enabled = true;
            setCursorPos(4);
            Menu3 m = (Menu3) menuScripts[2];
            if(m.isLoaded == false){
               m.loading();
            }
            m.updateNums();
        }
    }

    //when the defend option is chosen, will reduce the damage taken for the player
    public void defend(){
        playerScript.setDefending();
        ConversationManager.Instance.StartConversation(playerScript.battleTexts[3]);
    }

    //when an attack is chosen, will get the values for the attack and apply it to the enemy
    public void attack(int index, bool isPlayer){
        if(isPlayer){
            playerScript.stopDefending();
            if(index == 0){
                //first get the info about the attack from the player
                //then implement the math for attacking enemy
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[0]);
            }else if(index == 1){
                //get info about 2nd attack
                //implement math against enemy
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[1]);
            }else{
                //same as above
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[2]);
            }
            
        }else{
            if(index == 0){
                //Enemy attack 1
                ConversationManager.Instance.StartConversation(enemy.battleTexts[0]);
            }else{
                //Enemy attack 2
                ConversationManager.Instance.StartConversation(enemy.battleTexts[1]);
            }
        }
    }

    //when using an item, will make sure the effects of the item happen properly
    public void useItem(int used){
        playerScript.stopDefending();
        if(used == 0){
            //use item
            ConversationManager.Instance.StartConversation(playerScript.battleTexts[4]); //HP Dialogue
        }else if(used == 1){
            //use item
            ConversationManager.Instance.StartConversation(playerScript.battleTexts[5]); //FP Dialogue
        }else if(used == 2){
            //use item
            ConversationManager.Instance.StartConversation(playerScript.battleTexts[6]); //AttackBoostDialogue
        }else if(used == 3){
            //use item
            ConversationManager.Instance.StartConversation(playerScript.battleTexts[7]); //DefenseDialogue
        }else{
            //use item
            ConversationManager.Instance.StartConversation(playerScript.battleTexts[8]); //ThrowablesDialogue
        }

    }

    //when the run option is chosen, will attempt to run, and will randomly succeed or fail
    public void attemptToRun(bool isPlayer){
        System.Random rand = new System.Random();

            


        if(isPlayer){
            playerScript.stopDefending();
            if(rand.Next(1,101) <= 75){
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[9]);
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[10]);
            }

        }else{
            if(rand.Next(1,101) >= 80){
                ConversationManager.Instance.StartConversation(enemy.battleTexts[2]);
            }else{
                ConversationManager.Instance.StartConversation(enemy.battleTexts[3]);
            }
        }
    }


    public void setEnemy(int foe){
        if(foe == 0){
            enemy = GameObject.FindObjectOfType<Enemy1>();
        }else if(foe == 1){
            enemy = GameObject.FindObjectOfType<Enemy2>();
        }else{
            enemy = GameObject.FindObjectOfType<Enemy3>();
        }
    }


    //retrieves the position for a spot the cursor can be
    private Vector3 getSpotPos(string name){
        return GameObject.Find(name).GetComponent<Transform>().position;
    }

    //gets the canvas that controls what menu is to be displayed
    private GameObject getCanvas(string name){
        return GameObject.Find(name);
    }

    //sets the position for the cursor to be at
    private void setCursorPos(int index){
        cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[index].x, cursorSpaces[index].y, 0f);
    }

    private bool checkCursorPos(Vector3 checking){
        return cursor.GetComponent<Transform>().position == checking;
    }

    //returns the current x value for the cursors position
    private float getCursorX(){
        return cursor.GetComponent<Transform>().position.x;
    }

    //returns the current y value for the cursors position
    private float getCursorY(){
        return cursor.GetComponent<Transform>().position.y;
    }


    private void enemyDecisions(){
        System.Random rand = new System.Random();

        if(rand.Next(1, 101) <= 90){
            attack(rand.Next(0,4), false);
        }else{
            attemptToRun(false);
        }
        enemysTurn = true;
        turnCount++;
    }
}
