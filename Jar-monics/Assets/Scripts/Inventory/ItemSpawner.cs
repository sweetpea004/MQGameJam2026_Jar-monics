using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawned;
    public void SetSpawned(GameObject obj)
    {
        GetComponentInChildren<UnityEngine.UI.Image>().sprite = spawned.GetComponent<SpriteRenderer>().sprite;
        spawned = obj;
    }
    [SerializeField] private GameObject parentRoom;
    public void SetParent(GameObject obj)
    {
        parentRoom = obj;
    }
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        if (spawned.GetComponent<Item>() == null)
        {
            Debug.LogError("cannot spawn this object");
        }
    }

    public void Initialize(GameObject spawnItem)
    {
        spawned = spawnItem;
        GetComponentInChildren<UnityEngine.UI.Image>().sprite = spawned.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        // Debug.Log(box.OverlapPoint(GameManager.Instance.ViewportMousePos) + string.Format("({0})", GameManager.Instance.ViewportMousePos));

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.ViewportMousePos))
        {
            //do stuff
            Debug.Log("creating physical item: " + spawned.name);
            GameObject obj = Instantiate(spawned, GameManager.Instance.WorldMousePos, Quaternion.identity, null);
            obj.name = spawned.name.Insert(0, "GO");
            obj.SetActive(true);
            Item item = obj.GetComponent<Item>();
            item.SetDragging(true);
            // item.SetLastPoint(point);

            if (item.GetInfinity)
            {
                return;
            }

            InventorySystem.Instance.RemoveItemOne(item);
            // obj.transform.position = Vector3.zero;
        }
    }


}
