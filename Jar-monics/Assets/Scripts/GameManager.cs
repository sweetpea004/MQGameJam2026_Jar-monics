using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static GameManager Instance{
        get{
        if(singleton == null){
            Debug.LogError("uh oh");
        }
        return singleton;
        }
    }
    [SerializeField] private Room[] rooms;

    public Action<Room> screenTransition;
    private Room currentRoom;
    public Room GetCurrentRoom{
        get => currentRoom;
    }
    public Action<Item> OnItemStartedDragging;
    // public delegate void ScreenTransitionEventHandler();
    // public event ScreenTransitionEventHandler OnScreenTransition;

    private void Awake(){
        if(singleton == null){
            //assign
            singleton = this;
        }else{
            Destroy(this);
        }

        rooms = FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);
    }

    private void Start(){
        screenTransition += UpdateCurrentRoom;
    }

    void UpdateCurrentRoom(Room room){
        currentRoom = room;
    }
}

public enum Screen{
    UNASSIGNED,
    GARDEN,
    WORKSHOP,
    SHOWCASE,
    UNDERGROUND
}
