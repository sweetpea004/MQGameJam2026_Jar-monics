using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private GardenPlot[] groundPlots;

    private static GardenManager singleton;
    public static GardenManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("uh oh");
            }
            return singleton;
        }
    }

    private void Awake()
    {
        if (singleton == null)
        {
            //assign
            singleton = this;
        }
        else
        {
            Destroy(this);
        }

        groundPlots = FindObjectsByType<GardenPlot>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
    }

    public void AnyTouches(BoxCollider2D collider, PlantType type)
    {
        Debug.Log("touched");
        foreach (GardenPlot plot in groundPlots)
        {
            Debug.Log(plot.GetBounds.IsTouching(collider));
            if (plot.GetBounds.IsTouching(collider))
            {
                Debug.Log("attempting to plant seed");
                PlantSeed(plot, type);
            }
        }
    }
    public void AnyTouches(BoxCollider2D collider, ToolType type)
    {
        foreach (GardenPlot plot in groundPlots)
        {
            if (plot.GetBounds.IsTouching(collider))
            {
                Debug.Log("attempting to use tool");
                UseTool(plot, type);
            }
        }
    }

    [SerializeField] private GameObject gardenPlant;
    private void PlantSeed(GardenPlot plot, PlantType type)
    {
        GardenPlant plant = gardenPlant.GetComponent<GardenPlant>();
        plant.ChangeType(type);
        plot.SetPlant(plant);
        Instantiate(gardenPlant, plot.transform.position, Quaternion.identity, plot.transform);
    }


    private void UseTool(GardenPlot plot, ToolType type)
    {
        if (plot.GetPlant == null)
        {
            Debug.LogError("no plant planeted here");
            return;
        }

        if (type == ToolType.TROWEL)
        {
            InventorySystem.Instance.AddItemOne(plot.GetPlant.GetItem());
            plot.RemovePlant();
        }
        if (type == ToolType.WATERINGCAN)
        {
            plot.GetPlant.AdvanceStage();
        }
    }
}
