using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GardenPlant : MonoBehaviour
{
    private int stage = 0;
    public int GetStage
    {
        get => stage;
    }
    public void SetStage(int value)
    {
        Debug.Log("GP set stage " + value);
        stage = value;
    }
    [SerializeField] private PlantType type;
    public void ChangeType(PlantType newType)
    {
        type = newType;
    }
    public PlantType GetPlantType
    {
        get => type;
    }
    private new SpriteRenderer renderer;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        renderer.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];
    }

    private void Update()
    {
        if (GameManager.Instance.ClickMouse.WasPressedThisFrame() && box.OverlapPoint(GameManager.Instance.WorldMousePos))
        {
            AdvanceStage();
        }


        if (GameManager.Instance.ClickMouse.WasReleasedThisFrame())
        {

        }
    }

    public void AdvanceStage()
    {

        PlantStage plant = SOManager.Instance.GetPlant(type);

        Debug.Log("Advance");
        if(GetStage < plant.MaxStage)
        {
            SetStage(GetStage + 1);

            Debug.Log(GetStage);
            if(GetPlantType == PlantType.Foliage && GetStage > 1)
            {
                Debug.Log("random");
                int value = 0;
                if(Time.frameCount % 2 == 0)
                {
                    value = 2;
                }
                else
                {
                    value = 3;
                }
                Debug.Log(value);
                SetStage(value);
            }

            renderer.sprite = plant.StageSprites[GetStage];
        }        
    }

    public Plant GetItem()
    {

        GameObject obj = Instantiate(SOManager.Instance.Prefab);
        Plant plant = obj.GetComponent<Plant>();
        plant.PlantName = string.Format("{0}-Stage{1}", type, stage);
        plant.Type = type;
        plant.Stage = stage;
        return plant;
    }
}
