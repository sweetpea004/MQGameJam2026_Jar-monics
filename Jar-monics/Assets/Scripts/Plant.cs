using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Plant : Item
{
    [SerializeField] private PlantType type;
    public PlantType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    [SerializeField] private string plantName;
    public string PlantName
    {
        get => plantName;
        set => plantName = value;

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
    [SerializeField] private Tonality tonality = Tonality.Major;


    protected new void Awake()
    {
        switch (type)
        {
            case PlantType.Succulent:
                maxStage = 3;
                break;
            case PlantType.Foliage:
                maxStage = 3;
                break;
        }



        Debug.Log(gameObject.GetComponent<SpriteRenderer>() == null);

        sprite = gameObject.GetComponent<SpriteRenderer>();
        audio = gameObject.GetComponent<AudioSource>();

        SetSprite();
        SetMusic();

        List<Plant> allPlants = new List<Plant>();

        foreach(Plant plant in allPlants)
        {
            plant.PlayMusic();
        }
        PlayMusic();
    }

    public void Init(PlantType t, int stage)
    {
        type = t;
        this.stage = stage;
    }

    void SetSprite()
    {
        switch (type)
        {
            case PlantType.Fern:
                sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
                break;
            case PlantType.Succulent:
                sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
                break;
            case PlantType.Cactus:
                sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
                break;
            case PlantType.Moss:
                sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
                break;
            case PlantType.Foliage:
                if (stage < 2 || tonality == Tonality.Neutral || tonality == Tonality.Major)
                {
                    sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
                }
                else
                {
                    sprite.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage + 1];
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

    public void PlayMusic()
    {
        audio.time = 0;
        audio.Play();
    }

    public void StopMusic()
    {
        audio.Stop();
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

    public override void OnItemReleased()
    {
    }
}
