using UnityEditor.Analytics;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selectable : MonoBehaviour
{
    private bool clicked;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private PlayerInput input;
    private InputAction click;
    private InputAction move;
    private GameObject item;
    private Vector3 mousePos = new Vector3(0,0,0);

    Camera main;

    void OnClick(InputAction.CallbackContext context)
    {
        clicked = !clicked;
        Debug.Log(mousePos);
    }

    void OnEnable()
    {
        click.Enable();
        move.Enable();
    }

    void OnDisable()
    {
        click.Disable();
        move.Disable();
    }

    void Awake()
    {
        main = Camera.main;
        clicked = false;

        input = new PlayerInput();
        click = input.Player.Click;
        click.performed += OnClick;

        move = input.Player.Move;

    }

    void FixedUpdate()
    {
        if (clicked)
        {
            mousePos = move.ReadValue<Vector2>();
            mousePos = main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.5f);
        }
    }

    void Update()
    {        
        
    }
}