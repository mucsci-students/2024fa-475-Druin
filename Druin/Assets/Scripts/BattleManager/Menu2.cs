using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu2 : Menu
{
    // Start is called before the first frame update
    public override void handleInteraction(int pos){
        if(pos == 0){
            battleManager.attack(0, true);
        }
        if(pos == 1){
            battleManager.attack(1, true);
        }
        if(pos == 2){
            battleManager.attack(2, true);
        }
        if(pos == 3){
            battleManager.changeMenu(0);
        }
    }
}
