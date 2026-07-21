using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private GardenPlot[] groundPlots;
    [SerializeField] private GameObject blankGardenPlantPrefab;
    [SerializeField] private GameObject blankPlantPrefab;
    public GameObject GetBlankPlantObj
    {
        get => blankPlantPrefab;
    }

    private AudioSource audioSource;
    [SerializeField] private AudioClip blopSound;
    [SerializeField] private AudioClip wateringSound;
    [SerializeField] private AudioClip trowelSound;

    private static GardenManager singleton;
    public static GardenManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.Log("uh oh (garden manager)");
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
        audioSource = GetComponent<AudioSource>();
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
                // Sound Effect
                audioSource.PlayOneShot(blopSound);
            }
        }
    }
    public void AnyTouches(BoxCollider2D collider, ToolType type)
    {
        foreach (GardenPlot plot in groundPlots)
        {
            if (plot.GetBounds.IsTouching(collider))
            {
                Debug.Log("attempting to use tool" + type);
                UseTool(plot, type);
            }
        }
    }

    private void PlantSeed(GardenPlot plot, PlantType type)
    {
        GameObject plantObj = Instantiate(blankGardenPlantPrefab, plot.transform.position, Quaternion.identity, plot.transform);
        GardenPlant plantScript = plantObj.GetComponent<GardenPlant>();
        plantScript.Initialize(0, type);

        plot.SetPlant(plantScript);
    }
    private void UseTool(GardenPlot plot, ToolType type)
    {
        if (plot.GetPlant == null)
        {
            Debug.LogError("no plant planted here");
            return;
        }

        if (type == ToolType.TROWEL)
        {
            Plant item = plot.GetPlant.CreatePlantItem();
            Debug.Log("digging up " + item.name);
            InventorySystem.Instance.AddItemOne(item);
            plot.RemovePlant();
            // Sound Effect
            audioSource.PlayOneShot(trowelSound);
        }
        if (type == ToolType.WATERINGCAN)
        {
            plot.GetPlant.AdvanceStage();
            // Sound Effect
            audioSource.PlayOneShot(wateringSound);
        }
    }
}
