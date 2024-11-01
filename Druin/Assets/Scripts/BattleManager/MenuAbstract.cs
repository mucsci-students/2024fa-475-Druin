using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{

    //set up variables here for classes that inherit this one
    public BattleManager battleManager;
    // Start is called before the first frame update
    void Start(){
        battleManager = GameObject.FindObjectOfType<BattleManager>();
    }
    public abstract void handleInteraction(int pos);
}
