using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D box;


    private bool isDragged = false;
    private Vector3 mousePos;


    void Awake()
    {
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
        Vector2 mouse = GameManager.Instance.MoveMouse.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(mouse);
        
    if(GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(mousePos)){
        isDragged = true;
        GameManager.Instance.OnItemStartedDragging?.Invoke(this);
    }
        

        if(GameManager.Instance.ClickMouse.WasReleasedThisFrame()){
            isDragged = false;

        }
}

}
