using UnityEngine;

public class Bottle : Item
{
    [SerializeField] private GameObject pointPrefab;
    private bool playing = false;
    private Vector3 direction = new Vector3();
    private BottleType type;
    [SerializeField] private Plant[] plants = new Plant[3]; 

    protected new void Awake()
    {
        base.Awake();
        type = BottleType.Clear;
    }

    void Start()
    {
    }

    protected new void Update()
    {
        base.Update();
    }
}