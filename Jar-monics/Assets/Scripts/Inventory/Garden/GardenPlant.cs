using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GardenPlant : MonoBehaviour
{
    private int stage;
    public int GetStage
    {
        get => stage;
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
        if (!hasBeenInitCorrectly)
        {
            Debug.LogError("garden plant has not been created properly");
        }
        renderer.sprite = SOManager.Instance.GetPlant(type).StageSprites[stage];

    }
    private bool hasBeenInitCorrectly = false;
    public void Initialize(int stageValue, PlantType plantType)
    {
        hasBeenInitCorrectly = true;

        stage = stageValue;
        type = plantType;
        gameObject.name = string.Format("Garden{0}-Stage{1}", type, stage);
    }


    public void AdvanceStage()
    {
        PlantStage plant = SOManager.Instance.GetPlant(type);

        Debug.Log("Advanced");
        if (GetStage < plant.MaxStage)
        {
            stage++;

            //rng plant
            if (type == PlantType.Foliage && stage > 1)
            {
                Debug.Log("random");
                int value = 3;
                if (Time.frameCount % 2 == 0)
                {
                    value = 2;
                }
                Debug.Log(value);
                stage = value;
            }

            renderer.sprite = plant.StageSprites[GetStage];
        }
    }
    public Plant CreatePlantItem()
    {
        GameObject obj = Instantiate(GardenManager.Instance.GetBlankPlantObj, InventorySystem.Instance.transform);
        Plant plant = obj.GetComponent<Plant>();
        plant.Initialize(type, stage);

        InventorySystem.Instance.AddToCache(plant);

        obj.SetActive(false);

        return plant;
        // GameObject obj = Instantiate(SOManager.Instance.Prefab);
        // Plant plant = obj.GetComponent<Plant>();
        // plant.PlantName = string.Format("{0}-Stage{1}", type, stage);
        // plant.Type = type;
        // plant.Stage = stage;
        // return plant;
    }
}
