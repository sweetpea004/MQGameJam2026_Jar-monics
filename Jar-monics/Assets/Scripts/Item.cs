using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private Point lastPoint;
    private List<Point> newPoints = new List<Point>();

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point") && collision.gameObject.GetComponent<Point>().Occupant == null)
        {
            newPoints.Add(collision.gameObject.GetComponent<Point>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point"))
        {
            newPoints.Remove(collision.gameObject.GetComponent<Point>());
        }
    }

    protected void Awake()
    {
        input = new PlayerInput();
        click = input.Player.Click;
        move = input.Player.Move;

        cam = Camera.main;
        box = GetComponent<BoxCollider2D>();
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
        else if(lastPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, 0.2f);
        }
    }

    void DraggingItem(){
        Vector2 mouse = move.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(mouse);
        
        if(click.WasPressedThisFrame() && box.OverlapPoint(mousePos)){
            isDragged = true;
            lastPoint.Occupant = null;
        }
        

        if(click.WasReleasedThisFrame()){
            if (newPoints.Count > 0)
            {
                newPoints = newPoints.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
                lastPoint = newPoints.ElementAt(0);
            }
            isDragged = false;
            lastPoint.Occupant = this;
        }
    }
}
