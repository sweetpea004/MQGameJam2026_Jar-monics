using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static GameManager Instance{
        get{
        if(singleton == null){
            singleton = new GameManager();
        }
        return singleton;
        }
    }

    public Action<Room> screenTransition;
    // public delegate void ScreenTransitionEventHandler();
    // public event ScreenTransitionEventHandler OnScreenTransition;

    private void Awake(){
        if(singleton == null){
            //assign
            singleton = this;
        }else{
            Destroy(this);
        }
    }
}

public enum Screen{
    UNASSIGNED,
    GARDEN,
    WORKSHOP,
    SHOWCASE,
    UNDERGROUND
}
