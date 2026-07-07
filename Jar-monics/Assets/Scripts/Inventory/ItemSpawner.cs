using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawned;
    [SerializeField] private GameObject parentRoom;
    private BoxCollider2D box; 

    private void Awake(){
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Debug.Log(box.OverlapPoint(GameManager.Instance.WorldMousePos) + string.Format("({0})", GameManager.Instance.WorldMousePos));

        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            //do stuff
            GameObject obj = Instantiate(spawned, GameManager.Instance.WorldMousePos, Quaternion.identity, parentRoom.transform);
            Seed seed = obj.GetComponent<Seed>();
            seed.SetDragging(true);

            // obj.transform.position = Vector3.zero;
        }
    }
}
