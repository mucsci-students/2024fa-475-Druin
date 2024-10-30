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
        //player = FindObjectOfType<GameManager>().player;

        //used to get the position for the cursor to be at for the menu
        //invisible game objects are placed where the cursor will be when looking at a specific option
        cursorSpaces = new[] {getSpotPos("TriSpot1"), getSpotPos("TriSpot2"), getSpotPos("TriSpot3"), getSpotPos("TriSpot4")};
        

        menuCanvi = new[] {getCanvas("Menu1"), getCanvas("Menu2")};
        currentCanvas = menuCanvi[0];

        menuScripts = new Menu[] {GameObject.FindObjectOfType<Menu1>(), GameObject.FindObjectOfType<Menu2>(), GameObject.FindObjectOfType<Menu3>()};

        //this code finds the cursor game object and ensures that it is at the start position
        cursor = GameObject.FindWithTag("Cursor");
        setCursorPos(0);
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

            }else if(currentCanvas.name == "Menu3"){

            }else{
                if(checkCursorPos(cursorSpaces[0])){
                    menuScripts[0].handleInteraction(0);
                }else if(checkCursorPos(cursorSpaces[1])){
                    menuScripts[0].handleInteraction(1);
                }else if(checkCursorPos(cursorSpaces[2])){
                    menuScripts[0].handleInteraction(2);
                }else{
                    menuScripts[0].handleInteraction(4);
                }
            }
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
}
