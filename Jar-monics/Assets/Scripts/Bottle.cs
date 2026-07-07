using UnityEngine;

public class Bottle : Item
{
    [SerializeField] private GameObject pointPrefab;
    private bool playing = false;
    private Vector3 direction = new Vector3();
    private BottleType type;
    [SerializeField] private Vector3[] plantSpots = new Vector3[3];
    private bool[] occupancies = new bool[3];
    public int occupants = 0;

    protected new void Awake()
    {
        base.Awake();
        type = BottleType.Clear;
        plantSpots[1] = new Vector3(0, -1.5f);


        for(int i = 0; i < occupancies.Length; i++)
        {
            occupancies[i] = false;
        }
    }

    public void AddPlant(GameObject o)
    {
        occupants++;
        for(int i = 0; i < occupancies.Length; i++)
        {
            if(occupancies[i] == false)
            {
                occupancies[i] = true;
                o.transform.parent = gameObject.transform;
                o.transform.position = plantSpots[1];
                o.transform.localScale = new Vector3(0.5f, 0.5f);
                break;
            }
        }
    }

    void Start()
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