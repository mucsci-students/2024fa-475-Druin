using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu1 : Menu
{
    // Start is called before the first frame update
    public override void handleInteraction(int pos){
        if(pos == 0){
            battleManager.changeMenu(1);
        }
        if(pos == 1){
            battleManager.defend();
        }
        if(pos == 2){
            battleManager.changeMenu(2);
        }
        if(pos == 3){
            battleManager.attemptToRun(true);
        }
    }
}
