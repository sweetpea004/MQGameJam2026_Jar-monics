using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Item : MonoBehaviour
{
    private BoxCollider2D box;
    private Rigidbody2D body;
    public string GetName
    {
        get => gameObject.name;
    }
    private bool isDragged = false;
    [SerializeField] private float lerpSpeed = 10;
    private bool isInfinite = false;

    public void SetInfinity(bool value)
    {
        isInfinite = value;
    }
    public bool GetInfinity
    {
        get => isInfinite;
    }

    private Vector3 mousePos;
    [SerializeField] private Point lastPoint;
    public void SetLastPoint(Point point)
    {
        lastPoint = point;
    }
    public Point GetLastPoint
    {
        get => lastPoint;
    }
    private List<Point> newPoints = new List<Point>();
    
    public void SetDragging(bool value)
    {
        isDragged = value;
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
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Kinematic;
    }

    protected void Update()
    {
        DraggingItem();

        if (isDragged)
        {
            transform.position = GameManager.Instance.WorldMousePos;
            Debug.Log("Dragging" + gameObject.name);
        }

        if (!isDragged && lastPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, lerpSpeed * Time.deltaTime);
        }
    }

    void DraggingItem()
    {

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            isDragged = true;
            if(lastPoint != null)
            {
                lastPoint.Occupant = null;
            }
        }


        if (GameManager.Instance.ClickMouse.WasReleasedThisFrame())
        {
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

    public abstract void OnItemReleased();
    public abstract void OnItemSelected();
}
