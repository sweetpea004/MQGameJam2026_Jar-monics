using UnityEngine;

public class Plant : Item
{
    [SerializeField] private PlantStages stageSprite;
    public PlantStages StageSprites
    {
        get => stageSprite;
    }
    private int plantStage = 0;
    public int GetStage
    {
        get => plantStage;
    }
    private Vector3 direction;
    private PlantType type;

    public override void OnItemReleased()
    {
    }
    public override void OnItemSelected()
    {
    }
}
