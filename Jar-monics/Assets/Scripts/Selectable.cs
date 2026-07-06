using UnityEngine;
using UnityEngine.InputSystem;

public class Selectable : MonoBehaviour
{
    private bool clicked;

    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerInput input;
    private InputAction click;
    private InputAction move;
    private GameObject item;
    private Vector2 mousePos = new Vector2();

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
        clicked = false;

        input = new PlayerInput();
        click = input.Player.Click;
        click.performed += OnClick;

        move = input.Player.Move;

    }

    void Update()
    {
        mousePos = move.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos);
        
        if (clicked)
        {

        }
    }
}