using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour
{
    private BoxCollider2D room;
    [SerializeField] private Color colour = Color.blue;
private void OnDrawGizmos(){
    if(room == null){
        room = GetComponent<BoxCollider2D>();
    }
    Camera cam = Camera.main;
    float aspect = cam.aspect;
    float yHeight = cam.orthographicSize * 2;
    // room.bounds = new Bounds(transform.position, new Vector3(aspect * yHeight, yHeight));
    room.size = new Vector3(aspect * yHeight, yHeight);


    Gizmos.color = colour;
    Gizmos.DrawWireCube(transform.position, room.bounds.size);

}
}
