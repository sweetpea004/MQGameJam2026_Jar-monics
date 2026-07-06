using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Room startingRoom;
    private Room currentRoom;

    [SerializeField] private Room[] rooms;
    private PlayerInput actions;
    private InputAction moveLeft;
    private InputAction moveRight;
    // private InputAction moveUp;
    // private InputAction moveDown;

    private void Awake(){
        rooms = FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);
        currentRoom = startingRoom;
    }

    private void Start(){
        actions = new PlayerInput();
        moveLeft = actions.Camera.Left;
        moveRight = actions.Camera.Right;

        actions.Enable();
    }

    private void Update(){

moveInput();
moveCamera();

    }

    private void moveInput(){
                if(moveLeft.WasPressedThisFrame()){
                    Debug.Log(currentRoom);
            currentRoom = currentRoom.GoLeft;
            Debug.Log(currentRoom);
        }
        if(moveRight.WasPressedThisFrame()){
            Debug.Log(currentRoom);
            currentRoom = currentRoom.GoRight;
            Debug.Log(currentRoom);
        }
        //         if(moveUp.WasPressedThisFrame()){
        //             Debug.Log(currentRoom);
        //     currentRoom = currentRoom.GoUp;
        //     Debug.Log(currentRoom);
        // }
        //         if(moveDown.WasPressedThisFrame()){
        //             Debug.Log(currentRoom);
        //     currentRoom = currentRoom.GoDown;
        //     Debug.Log(currentRoom);
        // }
    }

    private void moveCamera(){
        
        transform.position = new Vector3(currentRoom.GetX, currentRoom.GetY, transform.position.z);
    }

}
