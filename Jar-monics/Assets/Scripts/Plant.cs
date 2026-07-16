using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class Plant : Item
{
    [SerializeField] private PlantType type;
    public PlantType GetPlantType
    {
        get => type;
    }
    [SerializeField] private int stage;
    public int GetStage
    {
        get => stage;
    }

    private AudioSource audio;

    private bool constructedProperly = false;

    [SerializeField] private readonly Tonality tonality = Tonality.Major;

    public void Initialize(PlantType plantType, int stageValue)
    {
        type = plantType;
        stage = stageValue;
        constructedProperly = true;

        gameObject.name = string.Format("{0}-Stage{1}", type, stage);

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("plant script has no renderer");
            return;
        }
        spriteRenderer.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage]; //set image

        //set bounding box size
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
        spriteSize = Vector3.Scale(spriteSize, spriteRenderer.transform.localScale);

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = spriteSize;
        collider.offset = Vector2.up * spriteSize.y / 2f;

        if (type == PlantType.Foliage && stage > 2 && tonality != Tonality.Minor)
        {
            spriteRenderer.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage + 1];
        }


        audio = gameObject.GetComponent<AudioSource>();
        SetMusic();
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

    public override void OnItemSelected()
    {
        if (!constructedProperly)
        {
            Debug.LogError("not created properly - plant ");
        }
    }

    public override void OnItemReleased()
    {
    }
}
