using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Item : MonoBehaviour
{
    protected BoxCollider2D box;
    private Rigidbody2D body;
    public string GetName
    {
        get => gameObject.name;
    }
    protected bool isDragged = false;
    private bool isInfinite = false;

    public void SetInfinity(bool value)
    {
        isInfinite = value;
    }
    public bool GetInfinity
    {
        get => isInfinite;
    }
    public void SetDragging(bool value)
    {
        isDragged = value;
    }

    protected void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        DraggingItem();
    }

    void DraggingItem()
    {

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            isDragged = true;
            OnItemSelected();
        }


        if (GameManager.Instance.ClickMouse.WasReleasedThisFrame())
        {
            isDragged = false;
            OnItemReleased();
        }

        if (isDragged)
        {
            Vector2 offsetPosition = GameManager.Instance.WorldMousePos;
            offsetPosition -= box.offset;
            transform.position = offsetPosition;
        }
    }

    public abstract void OnItemReleased();
    public abstract void OnItemSelected();
}
