using UnityEngine;

[CreateAssetMenu(fileName = "PlantStage", menuName = "Scriptable Objects/PlantStage")]
public class PlantStage : ScriptableObject
{
    [SerializeField] private Sprite[] stages = new Sprite[4];
    public Sprite[] Stages
    {
        get => stages;
    }
}   
