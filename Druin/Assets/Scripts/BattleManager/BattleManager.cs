using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject player;

    private GameObject enemy;

    private GameObject[] menuCanvi = new GameObject[3];

    private Menu[] menuScripts = new Menu[3];

    private Vector3[] cursorSpaces;

    private GameObject cursor;
    
    // Start is called before the first frame update
    
    void Start()
    {
        //player = FindObjectOfType<GameManager>().player;

        //used to get the position for the cursor to be at for the menu
        //invisible game objects are placed where the cursor will be when looking at a specific option
        cursorSpaces = new[] {getSpotPos("TriSpot1"), getSpotPos("TriSpot2"), getSpotPos("TriSpot3"), getSpotPos("TriSpot4")};

        //this code finds the cursor game object and ensures that it is at the start position
        cursor = GameObject.FindWithTag("Cursor");
        cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[0].x, cursorSpaces[0].y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //pressing the Up arrow key will send the cursor to the top row no matter what column it is in
        if(Input.GetKeyDown(KeyCode.UpArrow) && (cursor.GetComponent<Transform>().position.y == -4.21f)){
            if(cursor.GetComponent<Transform>().position.x == -3f){
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[0].x, cursorSpaces[0].y, 0f);
            }else{
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[2].x, cursorSpaces[2].y, 0f);
            }
        }

        //pressing the Down arrow key will send the cursor to the bottom row no matter what column it is in
        if(Input.GetKeyDown(KeyCode.DownArrow) && (cursor.GetComponent<Transform>().position.y == -2.84f)){
            if(cursor.GetComponent<Transform>().position.x == -3f){
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[1].x, cursorSpaces[1].y, 0f);
            }else{
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[3].x, cursorSpaces[3].y, 0f);
            }
        }

        //pressing the Right arrow key in the 1st column will send it to the 2nd column no matter the row
        if(Input.GetKeyDown(KeyCode.RightArrow) && (cursor.GetComponent<Transform>().position.x == -3f)){
            if(cursor.GetComponent<Transform>().position.y == -2.84f){
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[2].x, cursorSpaces[2].y, 0f);
            }else{
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[3].x, cursorSpaces[3].y, 0f);
            }
        }

        //pressing the Left arrow key in the 1st column will send it to the 2nd column no matter the row
        if(Input.GetKeyDown(KeyCode.LeftArrow) && (cursor.GetComponent<Transform>().position.x == 0.8f)){
            if(cursor.GetComponent<Transform>().position.y == -2.84f){
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[0].x, cursorSpaces[0].y, 0f);
            }else{
                cursor.GetComponent<Transform>().position = new Vector3(cursorSpaces[1].x, cursorSpaces[1].y, 0f);
            }
        }
    }

    private Vector3 getSpotPos(string name){
        return GameObject.Find(name).GetComponent<Transform>().position;
    }
}
