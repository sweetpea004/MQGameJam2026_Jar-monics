using UnityEngine;
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InventorySystem))]
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static GameManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("uh oh");
            }
            return singleton;
        }
    }
    [SerializeField] private Room[] rooms;
    [SerializeField] private GameObject pointObject;

    public GameObject GetPointObj
    {
        get => pointObject;
    }

    public Action<Room> screenTransition;
    private Room currentRoom;
    public Room GetCurrentRoom
    {
        get => currentRoom;
    }
    public Action<Item> OnItemStartedDragging;
    // public delegate void ScreenTransitionEventHandler();
    // public event ScreenTransitionEventHandler OnScreenTransition;


    private Camera cam;
    public Camera Camera
    {
        get => cam;
    }

    public PlayerInput actions;
    public InputAction ClickMouse { get; private set; }
    public InputAction MoveMouse { get; private set; }
    public Vector2 ViewportMousePos { get; private set; }
    public Vector2 WorldMousePos { get; private set; }

    private void Awake()
    {
        if (singleton == null)
        {
            //assign
            singleton = this;
        }
        else
        {
            Destroy(this);
        }

        rooms = FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);

        actions = new PlayerInput();
        ClickMouse = actions.Player.Click;
        MoveMouse = actions.Player.Move;

        cam = Camera.main;
    }

    private void Start()
    {
        screenTransition += UpdateCurrentRoom;
    }

    void OnEnable()
    {
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Disable();
    }


    void UpdateCurrentRoom(Room room)
    {
        currentRoom = room;
    }

    private void Update()
    {
        ViewportMousePos = MoveMouse.ReadValue<Vector2>();
        WorldMousePos = cam.ScreenToWorldPoint(ViewportMousePos);
    }
}

public enum Screen
{
    UNASSIGNED,
    GARDEN,
    WORKSHOP,
    SHOWCASE,
    UNDERGROUND
}
