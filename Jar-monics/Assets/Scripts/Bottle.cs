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

    protected void Start()
    {
    }

    protected new void Update()
    {
        base.Update();
    }

    public override void OnItemReleased()
    {
        transform.localScale = GetLastPoint.transform.localScale * 2;
    }
    public override void OnItemSelected()
    {
    }
}