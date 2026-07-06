using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D box;

    private PlayerInput input;
    private InputAction click;
    private InputAction move;

    private bool isDragged = false;
    private Vector3 mousePos;

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
        input = new PlayerInput();
        click = input.Player.Click;
        move = input.Player.Move;

cam = Camera.main;
        box = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        DraggingItem();

        if(isDragged){
            Vector2 position = mousePos;
            // position.z = 0;
            // Debug.Log(position);
            transform.position = position;
        }
    }

void DraggingItem(){
        Vector2 mouse = move.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(mouse);
        
    if(click.WasPressedThisFrame() && box.OverlapPoint(mousePos)){
        isDragged = true;
        GameManager.Instance.OnItemStartedDragging?.Invoke(this);
    }
        

        if(click.WasReleasedThisFrame()){
            isDragged = false;

        }
}

}
