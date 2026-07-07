using UnityEngine;

[RequireComponent(typeof(Border))]
public class Room : MonoBehaviour
{

    [SerializeField] private Room toLeft;
    [SerializeField] private Room toRight;
    [SerializeField] private Room toUp;
    [SerializeField] private Room toDown;


    public Room GoLeft
    {
        get => toLeft ?? this;
    }
    public Room GoRight
    {
        get => toRight ?? this;
    }
    public Room GoUp
    {
        get => toUp ?? this;
    }
    public Room GoDown
    {
        get => toDown ?? this;
    }
    public float GetX
    {
        get => transform.position.x;
    }

    public float GetY
    {
        get => transform.position.y;
    }
}
