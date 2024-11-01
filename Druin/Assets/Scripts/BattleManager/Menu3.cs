using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu3 : Menu
{

    private GameObject[] itemTexts;

    private PlayerScript player;
    
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindObjectOfType<PlayerScript>();
        
        GameObject textObj;

        //set up an items array list in playerscript then come back here
    }
    
    public override void handleInteraction(int pos){

    }
}
