using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject player;

    private GameObject enemy;

    private GameObject[] menuCanvi;

    private GameObject currentCanvas;

    private Menu[] menuScripts;

    private Vector3[] cursorSpaces;

    private GameObject cursor;
    
    // Start is called before the first frame update
    
    void Start()
    {
        

        //used to get the position for the cursor to be at for the menu
        //invisible game objects are placed where the cursor will be when looking at a specific option
        cursorSpaces = new[] {getSpotPos("TriSpot1"), getSpotPos("TriSpot2"), getSpotPos("TriSpot3"), getSpotPos("TriSpot4")};
        

        menuCanvi = new[] {getCanvas("Menu1"), getCanvas("Menu2"), getCanvas("Menu3")};
        currentCanvas = menuCanvi[0];

        menuScripts = new Menu[] {GameObject.FindObjectOfType<Menu1>(), GameObject.FindObjectOfType<Menu2>(), GameObject.FindObjectOfType<Menu3>()};

        //this code finds the cursor game object and ensures that it is at the start position
        cursor = GameObject.FindWithTag("Cursor");
        setCursorPos(0);

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //pressing the Up arrow key will send the cursor to the top row no matter what column it is in
        if(Input.GetKeyDown(KeyCode.UpArrow) && (getCursorY() == -4.21f)){
            if(getCursorX() == -3f){
                setCursorPos(0);
            }else{
                setCursorPos(2);
            }
        }

        //pressing the Down arrow key will send the cursor to the bottom row no matter what column it is in
        if(Input.GetKeyDown(KeyCode.DownArrow) && (getCursorY() == -2.84f)){
            if(getCursorX() == -3f){
                setCursorPos(1);
            }else{
                setCursorPos(3);
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
        if(Input.GetKeyDown(KeyCode.Space)){
            if(currentCanvas.name == "Menu2"){
                if(checkCursorPos(cursorSpaces[0])){
                    menuScripts[1].handleInteraction(0);
                }else if(checkCursorPos(cursorSpaces[1])){
                    menuScripts[1].handleInteraction(1);
                }else if(checkCursorPos(cursorSpaces[2])){
                    menuScripts[1].handleInteraction(2);
                }else{
                    menuScripts[1].handleInteraction(3);
                }
            }else if(currentCanvas.name == "Menu3"){

            }else{
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
        }
    }

    //changes the menu shown when required
    public void changeMenu(int index){
        //turns of the current menu
        currentCanvas.GetComponent<Canvas>().enabled = false;

        //switches to the next menu and activates it
        currentCanvas = menuCanvi[index];
        currentCanvas.GetComponent<Canvas>().enabled = true;
    }

    //when the defend option is chosen, will reduce the damage taken for the player
    public void defend(){
        player.GetComponent<PlayerScript>().setDefending();
    }

    //when an attack is chosen, will get the values for the attack and apply it to the enemy
    public void attack(int index){

    }

    //when using an item, will make sure the effects of the item happen properly
    public void useItem(){

    }

    //when the run option is chosen, will attempt to run, and will randomly succeed or fail
    public void attemptToRun(){

    }


    public void setEnemy(GameObject foe){
        enemy = foe;
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
}
