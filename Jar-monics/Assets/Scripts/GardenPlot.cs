using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GardenPlot : MonoBehaviour
{
    private BoxCollider2D box;
    private GardenPlant plant;
    public void SetPlant(GardenPlant plant)
    {
        this.plant = plant;
    }
    public GardenPlant GetPlant
    {
        get => plant;
    }

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    public BoxCollider2D GetBounds
    {
        get => box;
    }

    public void RemovePlant()
    {
        if (plant != null)
        {
            GardenPlant gPlant = GetComponentInChildren<GardenPlant>();
            Destroy(gPlant.gameObject);
        }
    }


}
