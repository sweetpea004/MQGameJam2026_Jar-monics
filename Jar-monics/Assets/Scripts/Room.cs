using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
private Collider2D room;
    [SerializeField] private Color colour = Color.blue;

    [SerializeField] private Room toLeft;
    [SerializeField] private Room toRight;
    [SerializeField] private Room toUp;
    [SerializeField] private Room toDown;

public Room GoLeft{
    get => toLeft ?? this;
}
public Room GoRight{
    get => toRight?? this;
}
public Room GoUp{
    get => toUp?? this;
}
public Room GoDown{
    get => toDown?? this;
}

public float GetX{
    get => transform.position.x;
}

public float GetY{
    get => transform.position.y;
}


private void OnDrawGizmos(){
    if(room == null){
room = GetComponent<Collider2D>();
    }

    Gizmos.color = colour;
    Gizmos.DrawWireCube(transform.position, room.bounds.size);

}
}
