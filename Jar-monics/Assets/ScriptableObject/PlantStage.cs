using UnityEngine;

[CreateAssetMenu(fileName = "PlantStage", menuName = "Scriptable Objects/PlantStage")]
public class PlantStage : ScriptableObject
{
    public int MaxStages
    {
        get => stages.Length;
    }

    [SerializeField] private Sprite[] stages = new Sprite[4];
    [SerializeField] private GameObject[] prefabs = new GameObject[4];
    public Sprite[] Stages
    {
        get => stages;
    }
    public GameObject[] Prefab
    {
        get => prefabs;
    }
}   
