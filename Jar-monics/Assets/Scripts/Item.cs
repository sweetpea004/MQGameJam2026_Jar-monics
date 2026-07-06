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
    private Point lastPoint;
    private Point newPoint;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point"))
        {
            newPoint = collision.gameObject.GetComponent<Point>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point"))
        {
            newPoint = null;
        }
    }

    protected void Awake()
    {
        input = new PlayerInput();
        click = input.Player.Click;
        move = input.Player.Move;

        cam = Camera.main;
        box = GetComponent<BoxCollider2D>();

        lastPoint = GameObject.Find("Point").GetComponent<Point>();
    }

    protected void Update()
    {
        DraggingItem();

        if (isDragged)
        {
            Vector3 position = mousePos;
            position.z = 0;
            transform.position = position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, 0.2f);
        }
    }

    void DraggingItem(){
        Vector2 mouse = move.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(mouse);
        
        if(click.WasPressedThisFrame() && box.OverlapPoint(mousePos)){
            isDragged = true;
        }
        

        if(click.WasReleasedThisFrame()){
            if (newPoint != null)
            {
                lastPoint = newPoint;
            }
            isDragged = false;
        }
    }
}
