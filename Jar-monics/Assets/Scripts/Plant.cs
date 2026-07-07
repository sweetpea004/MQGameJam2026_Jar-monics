using System;
using UnityEngine;

public class Plant : Item
{
    private PlantType type;
    public PlantType GetPlantType
    {
        get => type;
    }
    private SpriteRenderer sprite;

    [SerializeField] private int stage = 0;
    public int Stage
    {
        get
        {
            return stage;
        }

        set
        {
            stage = value;
        }
    }
    private int maxStage = 4; //All but two plants have 4 stages, the other two have had this value changed in Awake()
    public int GetMaxStage
    {
        get => maxStage;
    }
    [SerializeField] private bool isMaj = false;


    protected void Start()
    {
        if (type == PlantType.Foliage)
        {
            maxStage = 3;
        }

        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Init(PlantType t, bool isMajor)
    {
        type = t;
        isMaj = isMajor;
    }

    public override void OnItemSelected()
    {
    }
}
