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
        SetStage(GetStage + 1);
        SetStage(Mathf.Min(GetStage, plant.MaxStages - 1));
        Debug.Log(GetStage);
        if (type == PlantType.Foliage && GetStage > 2)
        {
            Debug.Log("random");
            int value = Random.Range(3, 4); //randomness;
            Debug.Log(value);
            SetStage(value);
        }
        renderer.sprite = plant.Stages[GetStage];
    }

    public Plant GetItem()
    {
        Debug.Log("st" + this.GetStage);
        Plant plat = SOManager.Instance.GetPlant(type).Prefab.GetComponent<Plant>();
        Debug.Log("st" + this.stage);
        plat.Stage = this.stage;
        return plat;
    }
}
