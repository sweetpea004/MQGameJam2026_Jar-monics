using UnityEngine;

public class Tool : Item
{
    [SerializeField] private ToolType toolType;
    protected void Start()
    {
        SetInfinity(true);
        SetLerpingToPoints(false);
    }
    public override void OnItemReleased()
    {
        GardenManager.Instance.AnyTouches(box, toolType);

        Destroy(gameObject);
    }
    public override void OnItemSelected()
    {
    }
}

public enum ToolType
{
    UNASSIGNED, 
    TROWEL,
    WATERINGCAN
}
