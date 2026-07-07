using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D box;
    private Rigidbody2D body;

    private PlayerInput input;
    private InputAction click;
    private InputAction move;

    private bool isDragged = false;
    [SerializeField] private float lerpSpeed = 10;
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

        if(collision.gameObject.layer == LayerMask.NameToLayer("Bottle") && gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            Bottle b = collision.gameObject.GetComponent<Bottle>();
            if(b.occupants < 3)
            {
                b.AddPlant(gameObject);
                lastPoint = null;
                gameObject.GetComponent<Collider2D>().enabled = false;
                isDragged = false;
            }
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
        body = GetComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Kinematic;
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
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, lerpSpeed * Time.deltaTime);
        }
    }

    void DraggingItem(){
        Vector2 mouse = move.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(mouse);
        
        if(click.WasPressedThisFrame() && box.OverlapPoint(mousePos)){
            isDragged = true;
            if(lastPoint != null)
            {
                lastPoint.Occupant = null;
            }
        }
        

        if(click.WasReleasedThisFrame()){
            if (newPoints.Count > 0)
            {
                newPoints = newPoints.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
                for(int i = 0; i < newPoints.Count; i++)
                {
                    if(gameObject.layer == LayerMask.NameToLayer("Bottle") && !newPoints.ElementAt(i).RejectBottle)
                    {
                        lastPoint = newPoints.ElementAt(i);
                        break;
                    }
                    else if(gameObject.layer == LayerMask.NameToLayer("Plant") && !newPoints.ElementAt(i).RejectPlant)
                    {
                        lastPoint = newPoints.ElementAt(i);
                        break;
                    }
                }
            }
            isDragged = false;

            if(lastPoint != null)
            {
                lastPoint.Occupant = this;
                transform.localScale = lastPoint.transform.localScale * 2;
            }
        }
    }
}
