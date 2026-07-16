using UnityEngine;
using System;

public class Seed : Item
{
    [SerializeField] private PlantType plant;
    protected void Start()
    {
        SetInfinity(true);
    }
    public override void OnItemReleased()
    {
        Debug.Log("items dropped");
        GardenManager.Instance.AnyTouches(box, plant);

        Destroy(gameObject);
    }
    public override void OnItemSelected()
    {
    }
}
