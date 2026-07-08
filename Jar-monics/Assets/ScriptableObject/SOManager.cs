using UnityEngine;

public class SOManager : MonoBehaviour
{
    private static SOManager singleton;
    public static SOManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.Log("uh oh");
            }
            return singleton;
        }
    }

    [SerializeField] private GameObject prefab;
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }

    private void Awake()
    {
        Debug.Log("Creating SOManager");
        if (singleton == null)
        {
            //assign
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] private PlantStage[] plants;
    public PlantStage GetPlant(PlantType plantType)
    {
        foreach (PlantStage stage in plants)
        {
            if (stage.name == plantType.ToString())
            {
                return stage;
            }
        }
        return null;
    }
}
