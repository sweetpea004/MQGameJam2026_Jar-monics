using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DebugButton : MonoBehaviour
{
    [SerializeField] private Item item;
    private BoxCollider2D box;
    protected void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            InventorySystem.Instance.AddItemOne(item);
        }
    }
}
