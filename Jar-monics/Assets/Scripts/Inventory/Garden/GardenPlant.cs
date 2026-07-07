using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GardenPlant : MonoBehaviour
{
    [SerializeField] private int stage = 0;
    [SerializeField] private PlantType type;
    public void ChangeType(PlantType newType)
    {
        type = newType;
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
        renderer.sprite = SOManager.Instance.GetPlant(type).Stages[stage];
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
        stage++;
        stage = Mathf.Min(stage, plant.MaxStages - 1);
        Debug.Log(stage);
        if (type == PlantType.Foliage && stage > 2)
        {
            stage = Random.Range(3, 4); //randomness
        }
        renderer.sprite = plant.Stages[stage];
    }

    public Item GetItem()
    {
        return SOManager.Instance.GetPlant(type).Prefab[stage].GetComponent<Plant>();
    }
}
