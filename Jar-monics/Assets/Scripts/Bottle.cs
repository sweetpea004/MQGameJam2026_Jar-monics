using UnityEngine;

public class Bottle : MonoBehaviour
{
    private Vector3 direction;
    private BottleType type;
    private Plant plant;

    void Awake()
    {
        type = BottleType.Clear;
    }

    void Start()
    {

    }

    void Update()
    {
    }
}