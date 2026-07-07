using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawned;
    [SerializeField] private GameObject parentRoom;
    private Point point;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        if (spawned.GetComponent<Item>() == null)
        {
            Debug.LogError("cannot spawn this object");
        }
    }
    private void Start()
    {
        GameObject pointObj = Instantiate(GameManager.Instance.GetPointObj, transform.position, Quaternion.identity, transform);
        point = pointObj.GetComponent<Point>();
    }

    private void Update()
    {
        Debug.Log(box.OverlapPoint(GameManager.Instance.ViewportMousePos) + string.Format("({0})", GameManager.Instance.ViewportMousePos));

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.ViewportMousePos))
        {
            //do stuff
            GameObject obj = Instantiate(spawned, GameManager.Instance.WorldMousePos, Quaternion.identity, parentRoom.transform);
            Item item = obj.GetComponent<Item>();
            item.SetDragging(true);
            item.SetLastPoint(point);

            if (item.GetInfinity)
            {
                return;
            }

            InventorySystem.Instance.RemoveItemOne(item);
            // obj.transform.position = Vector3.zero;
        }
    }


}
