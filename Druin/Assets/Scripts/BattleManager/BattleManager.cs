using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class BattleManager : MonoBehaviour
{
    /*
    The Paramaters for the battle manager

    all of these are private just to ensure that they can only change through
    the allowed methods
    */
    public bool isReady = false;


    //The turn count for battle
    private int turnCount;

    //determins if it is the enemies turn or not
    //is used to make sure battle messages progress properly
    private bool enemysTurn;

    private bool runSuccess;

    //both are used to interact with the player in the battle scene
    private GameObject player;
    private PlayerScript playerScript;

    //the specific enemy will be given, 
    //and this will store the script that holds its stats and moves
    private EnemyStats enemy;

    //used to hold each canvas that is used for the battle menu system
    private GameObject[] menuCanvi;

    //the currently displayed canvas
    private GameObject currentCanvas;

    //Holds all the scripts tied to each battle menu
    private Menu[] menuScripts;

    //Placeholders for the cursor, 
    //also used to determine where the cursor is when selecting an option in the menu
    private Vector3[] cursorSpaces;

    //The cursor
    private GameObject cursor;

    private GameObject[] sprites;

    
    // Start is called before the first frame update
    
    void Start()
    {
        turnCount = 1;
        enemysTurn = false;
        runSuccess = false;

        //used to get the position for the cursor to be at for the menu
        //invisible game objects are placed where the cursor will be when looking at a specific option
        cursorSpaces = new[] {getSpotPos("TriSpot1"), getSpotPos("TriSpot2"), getSpotPos("TriSpot3"), getSpotPos("TriSpot4"),
                                getSpotPos("ItemSpot1"), getSpotPos("ItemSpot2"), getSpotPos("ItemSpot3"), getSpotPos("ItemSpot4"),
                                getSpotPos("ItemSpot5"), getSpotPos("ItemSpot6")};
        

        //Collects the battle menus
        //then assigns the default canvas
        menuCanvi = new[] {getCanvas("Menu1"), getCanvas("Menu2"), getCanvas("Menu3"), getCanvas("Stats")};
        currentCanvas = menuCanvi[0];

        menuScripts = new Menu[] {GameObject.FindObjectOfType<Menu1>(), GameObject.FindObjectOfType<Menu2>(), GameObject.FindObjectOfType<Menu3>()};

        sprites = new[] {GameObject.Find("PumpkinLight")
                            ,GameObject.Find("PumpkinDark")
                            ,GameObject.Find("GhostLight")
                            ,GameObject.Find("GhostDark")
                            ,GameObject.Find("BatLight")
                            ,GameObject.Find("BatDark")};

        //this code finds the cursor game object and ensures that it is at the start position
        cursor = GameObject.FindWithTag("Cursor");
        setCursorPos(0);

        //finds the player object
        player = GameObject.Find("Player");
        //gets the playerScript from the player
        playerScript = player.GetComponent<PlayerScript>();
        //used to load the battle messages in the player script
        //also used to assign which enemy will be used in battle
        playerScript.loadBattleTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReady){
            isReady = true;
            updateNums();
            updatePos(true);
        }

        /*The start of this monstrosity controls the movement of the cursor.
        Whenever an option is chosen that would end the players turn,
        A message will be displayed which will pause interaction with the menus
        */
        if(ConversationManager.Instance != null && !ConversationManager.Instance.IsConversationActive){


            //in menu 1 or 2, pressing the up key will ensure that the cursor goes to the top row
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
                if(getCursorY() == -4.21f){
                    if(getCursorX() == -3f){
                        setCursorPos(0);
                    }else{
                        setCursorPos(2);
                    }
                }

                //This is for menu 3, it allows to cycle up through each item
                if(getCursorY() == 2f){
                    setCursorPos(4);
                }
                else if(getCursorY() == 1.05f){
                    setCursorPos(5);
                }
                else if(getCursorY() == .1f){
                    setCursorPos(6);
                }
                else if(getCursorY() == -.88f){
                    setCursorPos(7);
                }
                else if(getCursorY() == -2f){
                    setCursorPos(8);
                }
                
            }

            //In menu 1 or 2, will ensure that the cursor goes to the bottom row
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                if(getCursorY() == -2.84f){
                    if(getCursorX() == -3f){
                        setCursorPos(1);
                    }else{
                        setCursorPos(3);
                    }
                }

                //used for menu 3, will allow to cycle downward through the items
                if(getCursorY() == -.88f){
                    setCursorPos(9);
                }
                else if(getCursorY() == .1f){
                    setCursorPos(8);
                }
                else if(getCursorY() == 1.05f){
                    setCursorPos(7);
                }
                else if(getCursorY() == 2f){
                    setCursorPos(6);
                }
                else if (getCursorY() == 2.95f){
                    setCursorPos(5);
                }
                
            }

            //for menu 1 or 2, will ensure that the cursor goes to the 2nd column
            if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (getCursorX() == -3f)){
                if(getCursorY() == -2.84f){
                    setCursorPos(2);
                }else{
                    setCursorPos(3);
                }
            }

            //for menu 1 or 2, will ensure that the cursor goes to the 1st column
            if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (getCursorX() == 0.8f)){
                if(getCursorY() == -2.84f){
                    setCursorPos(0);
                }else{
                    setCursorPos(1);
                }
            }

            //within any menu, will call for the interaction from the menu
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){

                //will only effect menu 1
                if(currentCanvas.name == "Menu1"){

                    //This will call for the switching of the menu to menu2
                    if(checkCursorPos(cursorSpaces[0])){
                        menuScripts[0].handleInteraction(0);

                        //This will call for the player to defend
                    }else if(checkCursorPos(cursorSpaces[1])){
                        menuScripts[0].handleInteraction(1);

                        //This will call for the menu to be switched to menu3
                    }else if(checkCursorPos(cursorSpaces[2])){
                        menuScripts[0].handleInteraction(2);

                        //this will call for the player to attempt to run from the battle
                    }else{
                        menuScripts[0].handleInteraction(3);
                    }
                }

                //will only effect menu 2
                else if(currentCanvas.name == "Menu2"){

                    //this option will call for the player to use attack 1
                    if(checkCursorPos(cursorSpaces[0])){
                        menuScripts[1].handleInteraction(0);

                        //this option will call for the player to use attack 2
                    }else if(checkCursorPos(cursorSpaces[1])){
                        menuScripts[1].handleInteraction(1);

                        //this option will call for the player to use attack 3
                    }else if(checkCursorPos(cursorSpaces[2])){
                        menuScripts[1].handleInteraction(2);

                        //this option will switch the menu back to menu 1
                    }else{
                        menuScripts[1].handleInteraction(3);
                    }
                } 

                //this will only effect menu3 (the item menu)
                else if(currentCanvas.name == "Menu3"){
                    if(checkCursorPos(cursorSpaces[4])){

                        //will call for the player to use a healing potion
                        menuScripts[2].handleInteraction(4);
                    }else if(checkCursorPos(cursorSpaces[5])){

                        //will call for the player to use an fp restore
                        menuScripts[2].handleInteraction(5);
                    }else if(checkCursorPos(cursorSpaces[6])){

                        //will call for the player to use an attack boosting item
                        menuScripts[2].handleInteraction(6);
                    }else if(checkCursorPos(cursorSpaces[7])){

                        //will call for the player to use a defense boosting item
                        menuScripts[2].handleInteraction(7);
                    }else if(checkCursorPos(cursorSpaces[8])){

                        //will call for the player to use a throwable item
                        menuScripts[2].handleInteraction(8);
                    }else{

                        //will call for the menu to switch back to menu1
                        menuScripts[2].handleInteraction(9);
                    }
                }
            }
        }else{
                    //The interaction with the battle messages
                    //when a message is displayed this will handle continuing on
                    //to ensure that the player goes second, the enemies decision making will go as soon as the
                    //players message is dismissed
                    if(!enemysTurn){
                        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
                        ConversationManager.Instance.PressSelectedOption();
                        updateNums();
                        changeMenu(0);
                        if(runSuccess == true){
                            endBattle(false);
                        }else{
                            enemyDecisions();
                        }
                        }

                        //will handle interaction with the enemy battle messages
                    }else{
                        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
                        enemysTurn = false;
                        ConversationManager.Instance.PressSelectedOption();
                        updateNums();
                        if(playerScript.hp <= 0 || runSuccess == true){
                            endBattle(false);
                        }
                        }
                    }
            
        }
    }

    //When called switches the menu to the one desired
    public void changeMenu(int index){
        
        //this checks to see if menu3 is active
        //if it is turns off the background for the item menu
        if(currentCanvas.name == "Menu3"){
            GameObject.Find("RectangleBox").GetComponent<SpriteRenderer>().enabled = false;
            menuCanvi[3].GetComponent<Canvas>().enabled = true;
        }

        //disables the current battle menu
        currentCanvas.GetComponent<Canvas>().enabled = false;

        //switches to the next battle menu and activates it
        currentCanvas = menuCanvi[index];
        currentCanvas.GetComponent<Canvas>().enabled = true;

        //ensures that the cursor is reset to position 0
        setCursorPos(0);

        //if the menu that has been activated is menu3
        //will activate the item menu background
        //ensures that the cursor is at the initial position for the item menu
        if(currentCanvas.name == "Menu3"){
            menuCanvi[3].GetComponent<Canvas>().enabled = false;
            GameObject.Find("RectangleBox").GetComponent<SpriteRenderer>().enabled = true;
            setCursorPos(4);

            //allows for menu3 to find the items display objects
            //then updates the numbers to reflect the players inventory
            Menu3 m = (Menu3) menuScripts[2];
            if(m.isLoaded == false){
               m.loading();
            }
            m.updateNums();
        }
    }

    //will call for the player to increase their defense
    public void defend(){
        playerScript.setDefending();

        //"starts a conversation" to display that the player is defending
        ConversationManager.Instance.StartConversation(playerScript.battleTexts[3]);
    }

    //will call for the specific attack details from the player/enemy
    public void attack(int index, bool isPlayer){

        //the If is to allow both the player and enemy to use this method
        if(isPlayer){

            //ensures that if the player was defending last turn that it will be turned off
            playerScript.stopDefending();
            Attacks a;
            int FPCost;

            //will call for the information of attack 1 from the player
            if(index == 0){
                a = playerScript.attacks[0];
                int damage = a.damage;
                damage -= enemy.Defense;
                if(damage > 0){
                    enemy.HP -= damage;
                }

                ConversationManager.Instance.StartConversation(playerScript.battleTexts[0]);

                //will call for the information of attack 2 from the player
            }else if(index == 1){
                a = playerScript.attacks[1];
                FPCost = a.fpCost;
                if((playerScript.fp - FPCost) >= 0){
                    int damage = a.damage;
                    damage -= enemy.Defense;
                    if(damage > 0){
                        enemy.HP -= damage;
                    }

                    playerScript.fp -= FPCost;
                    ConversationManager.Instance.StartConversation(playerScript.battleTexts[1]);
                }else{
                    ConversationManager.Instance.StartConversation(playerScript.battleTexts[11]);
                }
                

                //will call for the information of attack 3 from the player
            }else if(index == 2){
                a = playerScript.attacks[2];
                FPCost = a.fpCost;
                if((playerScript.fp - FPCost) >= 0){
                    int damage = a.damage;
                    damage -= enemy.Defense;
                    if(damage > 0){
                        enemy.HP -= damage;
                    }

                    playerScript.fp -= FPCost;
                    ConversationManager.Instance.StartConversation(playerScript.battleTexts[2]);
                }else{
                    ConversationManager.Instance.StartConversation(playerScript.battleTexts[11]);
                }
                
            }
            
        }else{
            if(index == 0){
                Attacks a = enemy.attacks[0];
                int damage = a.damage;
                damage -= playerScript.defense;
                if(damage > 0){
                    playerScript.hp -= damage;
                }
                ConversationManager.Instance.StartConversation(enemy.battleTexts[0]);
            }else if(index == 1){
                Attacks a = enemy.attacks[1];
                int damage = a.damage;
                damage -= playerScript.defense;
                if(damage > 0){
                    playerScript.hp -= damage;
                }
                ConversationManager.Instance.StartConversation(enemy.battleTexts[1]);


                //These next two options will only apply to the boss
            }else if(index == 2){
                Attacks a = enemy.attacks[2];
                int damage = a.damage;
                damage += enemy.HP;
                ConversationManager.Instance.StartConversation(enemy.battleTexts[2]);
            }else if(index == 3){
                if(enemy.windUpAttack == false){
                    enemy.windUpAttack = true;
                    enemy.windUpTurn = (turnCount + 2);
                    ConversationManager.Instance.StartConversation(enemy.battleTexts[3]);
                }else{
                    Attacks a = enemy.attacks[3];
                    int damage = a.damage;

                    damage -= playerScript.defense;
                    if(damage > 0){
                        playerScript.hp -= damage;
                    }
                    ConversationManager.Instance.StartConversation(enemy.battleTexts[4]);
                }
            }
        }

        //Create a method to update the healthbar for the player/enemy

    }

    //calls for the items effect, and applies it to the player
    public void useItem(int used){
        playerScript.stopDefending();
        if(used == 0){
            if(playerScript.itemAmount() != 0 && playerScript.useItem(itemAffect.HealthPotion)){
                Item item = Items.itemInfo[0];
                playerScript.hp += item.value;
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[4]); //HP Dialogue
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[12]);
            }
            
        }else if(used == 1){
            if(playerScript.itemAmount() != 0 && playerScript.useItem(itemAffect.FPPotion)){
                Item item = Items.itemInfo[1];
                playerScript.fp += item.value;
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[5]); //FP Dialogue
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[12]);
            }
            
        }else if(used == 2){
            if(playerScript.itemAmount() != 0 && playerScript.useItem(itemAffect.StatBoostAttack)){
                playerScript.boostAttack();
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[6]); //AttackBoostDialogue
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[12]);
            }
            
        }else if(used == 3){
            if(playerScript.itemAmount() != 0 && playerScript.useItem(itemAffect.StatBoostAttack)){
                playerScript.boostDefense();
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[7]); //DefenseDialogue
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[12]);
            }
            
        }else{
            if(playerScript.itemAmount() != 0 && playerScript.useItem(itemAffect.Throwable)){
                Item item = Items.itemInfo[1];
                enemy.HP -= item.value;
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[8]); //ThrowablesDialogue
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[12]);
            }
            
        }

        //creat method to update bars for player and enemy

    }

    //when the run option is chosen, will attempt to run, and will randomly succeed or fail
    public void attemptToRun(bool isPlayer){
        System.Random rand = new System.Random();

            


        if(isPlayer){
            playerScript.stopDefending();
            if(rand.Next(1,101) <= 75){
                runSuccess = true;
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[9]);
            }else{
                ConversationManager.Instance.StartConversation(playerScript.battleTexts[10]);
            }

        }else{
            if(rand.Next(1,101) >= 80){
                runSuccess = true;
                ConversationManager.Instance.StartConversation(enemy.battleTexts[2]);
            }else{
                ConversationManager.Instance.StartConversation(enemy.battleTexts[3]);
            }
        }
    }


    public void setEnemy(int foe, bool isDark){
        if(enemy != null){
            updatePos(false);
        }
        if(foe == 0){
            enemy = GameObject.FindObjectOfType<Enemy1>(true);
            enemy.isDark = isDark;
        }else if(foe == 1){
            enemy = GameObject.FindObjectOfType<Enemy2>(true);
            enemy.isDark = isDark;
        }else if(foe == 2){
            enemy = GameObject.FindObjectOfType<Enemy3>(true);
            enemy.isDark = isDark;
        }else if(foe == 3){
            enemy = GameObject.FindObjectOfType<Boss>(true);
        }
        isReady = false;
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


    private void updateNums(){
        GameObject.Find("PlayerHPNum").GetComponent<Text>().text = Convert.ToString(playerScript.hp) + "/" + Convert.ToString(playerScript.maxHP);
        GameObject.Find("PlayerFPNum").GetComponent<Text>().text = Convert.ToString(playerScript.fp) + "/" + Convert.ToString(playerScript.maxFP);
        GameObject.Find("EnemyHPNum").GetComponent<Text>().text = Convert.ToString(enemy.HP);
        GameObject.Find("PlayerEXPNum").GetComponent<Text>().text = Convert.ToString(playerScript.exp) 
                                                                    + "/" + Convert.ToString(playerScript.toNextLevel);
        GameObject.Find("TurnNum").GetComponent<Text>().text = Convert.ToString(turnCount);
    }

    private void updatePos(bool a){
        if(enemy == GameObject.FindObjectOfType<Enemy1>(true)){
            if(!enemy.isDark){
                sprites[0].GetComponent<SpriteRenderer>().enabled = a;
            }else{
                sprites[1].GetComponent<SpriteRenderer>().enabled = a;
            }
        }else if(enemy == GameObject.FindObjectOfType<Enemy2>(true)){
            if(!enemy.isDark){
                sprites[2].GetComponent<SpriteRenderer>().enabled = a;
            }else{
                sprites[3].GetComponent<SpriteRenderer>().enabled = a;
            }
        }else if(enemy == GameObject.FindObjectOfType<Enemy1>(true)){
            if(!enemy.isDark){
                sprites[4].GetComponent<SpriteRenderer>().enabled = a;
            }else{
                sprites[5].GetComponent<SpriteRenderer>().enabled = a;
            }
        }
    }

    private void endBattle(bool playerWon){
        enemy.resetStats();
        if(playerWon){
            playerScript.getEXP(enemy.EXP);
        }

        // MDF: Lose a battle
        if (playerScript.hp <= 0)
        {
            playerScript.hp = playerScript.maxHP;
            GameObject.FindGameObjectWithTag("Player").transform.position =
            new Vector3(-36.6234245f,-14.368206f,0f);
        }
        else if (enemy.isBoss)
        {
            // Defeat boss
            GameObject.FindGameObjectWithTag("Player").transform.position =
            new Vector3(-260.429993f,101.160004f,0f);
        }
        
        WorldManager wm = FindObjectOfType<WorldManager>();
        wm.SwitchWorldFading(wm.world_before_battle);
    }

    private void enemyDecisions(){
        System.Random rand = new System.Random();
        if(enemy.HP <= 0){
            endBattle(true);
            return;
        }

        if(enemy.isBoss == false){
            
            if(rand.Next(1, 101) <= 90){
                attack(rand.Next(0,2), false);
            }else{
                attemptToRun(false);
            }
        }else{
            if(enemy.windUpAttack == true){
                if(enemy.windUpTurn == turnCount){
                    attack(4,false);
                }else{
                    ConversationManager.Instance.StartConversation(enemy.battleTexts[4]);
                }
            }else{
                attack(rand.Next(0,4), false);
            }
            
        }
        
        enemysTurn = true;
        turnCount++;
    }
}
