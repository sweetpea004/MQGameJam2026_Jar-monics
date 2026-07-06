using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = new GameManager();
        }
    }

    public Action<Room> screenTransition;
    // public delegate void ScreenTransitionEventHandler();
    // public event ScreenTransitionEventHandler OnScreenTransition;

    private void Awake(){
        if(Instance == null){
            //assign
            Instance = this;
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
