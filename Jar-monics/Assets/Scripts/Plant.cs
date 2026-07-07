using System;
using UnityEngine;

public class Plant : Item
{
    private String plantTypeString;
    private PlantType type;
    private SpriteRenderer sprite;

    private int stage = 0;
    private int maxStage = 4; //All but two plants have 4 stages, the other two have had this value changed in Awake()
    private bool isFoliageMaj;
    

    protected new void Awake()
    {
        base.Awake();
        type = PlantType.Fern;        

        
        switch (type)
        {
            case PlantType.Fern:
                plantTypeString = "Fern";
                break;
            case PlantType.Succulent:
                plantTypeString =  "Succulent";
                maxStage = 3;
                break;
            case PlantType.Cactus:
                plantTypeString = "Cactus";
                break;
            case PlantType.Moss:
                plantTypeString = "Moss";
                break;
            case PlantType.Foliage:
                plantTypeString = "Foliage";
                maxStage = 3;
                
                break;
        }

        sprite = gameObject.GetComponent<SpriteRenderer>();

        SetSprite();
    }

    void SetSprite()
    {

        GameObject sprites = GameObject.Find(plantTypeString);
        
        switch (type)
        {
            case PlantType.Fern:
                sprite.sprite = sprites.GetComponent<Fern>().Stages[stage];
                break;
            case PlantType.Succulent:
                sprite.sprite = sprites.GetComponent<Succulent>().Stages[stage];
                break;
            case PlantType.Cactus:
                sprite.sprite = sprites.GetComponent<Cactus>().Stages[stage];
                break;
            case PlantType.Moss:
                sprite.sprite = sprites.GetComponent<Moss>().Stages[stage];
                break;
            case PlantType.Foliage:
                if(stage < 2 || isFoliageMaj)
                {
                    sprite.sprite = sprites.GetComponent<Foliage>().Stages[stage];    
                }
                else
                {
                    sprite.sprite = sprites.GetComponent<Foliage>().Stages[stage + 1];
                }
                        
                break;
        }
    }

    void Grow()
    {
        if(stage < maxStage)
        {
            stage++;
        }

        SetSprite();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
