using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item spawned;
    private BoxCollider2D box; 

    private void Awake(){
        box = GetComponent<BoxCollider2D>();
    }

    private void Update(){

        if(GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.MousePos)){
            //do stuff
        }
    }
}
