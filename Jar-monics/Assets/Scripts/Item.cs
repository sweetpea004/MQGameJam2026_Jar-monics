using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Item : MonoBehaviour
{
    private BoxCollider2D box;
    private string itemName;
    public string GetName
    {
        get => itemName;
    }
    private bool isDragged = false;
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

        itemName = gameObject.name;

    }

    protected void Update()
    {
        DraggingItem();

        if (isDragged)
        {
            transform.position = GameManager.Instance.WorldMousePos;
        }

        if (!isDragged && lastPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, 0.2f);
        }
    }

    void DraggingItem()
    {

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            isDragged = true;
            lastPoint.Occupant = null;
            OnItemSelected();
        }


        if (GameManager.Instance.ClickMouse.WasReleasedThisFrame())
        {
            if (newPoints.Count > 0)
            {
                newPoints = newPoints.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
                lastPoint = newPoints.ElementAt(0);
            }
            isDragged = false;
            lastPoint.Occupant = this;
            OnItemReleased();
        }
    }

    public abstract void OnItemReleased();
    public abstract void OnItemSelected();
}
