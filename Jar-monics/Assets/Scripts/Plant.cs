using System;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : Item
{
    private PlantType type;
    public PlantType GetPlantType
    {
        get => type;
    }
    public new string GetName
    {
        get => string.Format("{0}-Stage{1}", type, stage);
    }
    private SpriteRenderer sprite;
    private AudioSource audio;

    [SerializeField] private int stage = 0;
    public int Stage
    {
        get
        {
            return stage;
        }

        set
        {
            Debug.Log("setting stage to " + value);
            stage = value;
        }
    }
    private int maxStage = 4; //All but two plants have 4 stages, the other two have had this value changed in Awake()
<<<<<<< HEAD
    public int GetMaxStage
    {
        get => maxStage;
    }
    [SerializeField] private bool isMaj = false;
=======
    [SerializeField] private Tonality tonality = Tonality.Neutral;
>>>>>>> 1bc4d69 (Added Major, Minor and Neutral track sets for each plant type)


    protected void Start()
    {
        if (type == PlantType.Foliage)
        {
            maxStage = 3;
        }

        sprite = gameObject.GetComponent<SpriteRenderer>();
<<<<<<< HEAD
=======
        audio = gameObject.GetComponent<AudioSource>();
 
        SetSprite();
>>>>>>> 1bc4d69 (Added Major, Minor and Neutral track sets for each plant type)
    }

    public void Init(PlantType t)
    {
        type = t;
    }
<<<<<<< HEAD
=======

    void SetSprite()
    {
        switch (type)
        {
            case PlantType.Fern:
                sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
                break;
            case PlantType.Succulent:
                sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
                break;
            case PlantType.Cactus:
                sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
                break;
            case PlantType.Moss:
                sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
                break;
            case PlantType.Foliage:
                if (stage < 2 || tonality == Tonality.Neutral || tonality == Tonality.Major)
                {
                    sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
                }
                else
                {
                    sprite.sprite = SOManager.Instance.GetPlant(type).Stages[stage + 1];
                }

                break;
        }
    }

    void SetMusic()
    {
        switch (tonality)
        {
            case Tonality.Major:
                audio.clip = SOManager.Instance.GetPlant(type).MajorTracks[stage];
                break;

            case Tonality.Neutral:
                audio.clip = SOManager.Instance.GetPlant(type).NeutralTracks[stage];
                break;

            case Tonality.Minor:
                audio.clip = SOManager.Instance.GetPlant(type).MinorTracks[stage];
                break;
        }
    }

    void Grow()
    {
        if (stage < maxStage)
        {
            stage++;
        }

        SetSprite();
    }

    void FixedUpdate()
    {
        SetSprite();
    }

    // Update is called once per frame
    protected new void Update()
    {

    }
    public override void OnItemSelected()
    {
        base.Update();
    }

>>>>>>> 1bc4d69 (Added Major, Minor and Neutral track sets for each plant type)
    public override void OnItemReleased()
    {
    }

    public override void OnItemSelected()
    {
    }
}
