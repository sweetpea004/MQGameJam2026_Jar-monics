using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float transitionSpeed = 10;
    [SerializeField] private Room startingRoom;
    [SerializeField] private Room currentRoom;

    private PlayerInput actions;
    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction moveUp;
    private InputAction moveDown;

    private void Awake()
    {
        currentRoom = startingRoom;
    }

    private void Start()
    {
        actions = new PlayerInput();
        moveLeft = actions.Camera.Left;
        moveRight = actions.Camera.Right;
        moveUp = actions.Camera.Up;
        moveDown = actions.Camera.Down;

        actions.Enable();
    }

    private void Update()
    {

        moveInput();
        moveCamera();

    }

    private void moveInput()
    {
        if (moveLeft.WasPressedThisFrame())
        {
            currentRoom = currentRoom.GoLeft;
            GameManager.Instance.screenTransition?.Invoke(currentRoom);
        }
        if (moveRight.WasPressedThisFrame())
        {
            currentRoom = currentRoom.GoRight;
            GameManager.Instance.screenTransition?.Invoke(currentRoom);
        }
        if (moveUp.WasPressedThisFrame())
        {
            currentRoom = currentRoom.GoUp;
            GameManager.Instance.screenTransition?.Invoke(currentRoom);
        }
        if (moveDown.WasPressedThisFrame())
        {
            currentRoom = currentRoom.GoDown;
            GameManager.Instance.screenTransition?.Invoke(currentRoom);
        }
    }

    private void moveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentRoom.GetX, currentRoom.GetY, transform.position.z), transitionSpeed * Time.deltaTime);
    }

}
