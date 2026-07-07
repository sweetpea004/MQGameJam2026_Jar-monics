using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    private BoxCollider2D box;


    private bool isDragged = false;
    
    public void SetDragging(bool value)
    {
            isDragged = value;
    }


    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        DraggingItem();
        // Debug.Log(isDragged);
        if(isDragged){
            transform.position = GameManager.Instance.WorldMousePos;
        }
    }

void DraggingItem(){
           
    if(GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos)){
        isDragged = true;
    }
        if(GameManager.Instance.ClickMouse.WasReleasedThisFrame()){
            Debug.Log("released");
            isDragged = false;

        }
}

}
