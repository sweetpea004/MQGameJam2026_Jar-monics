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

    [SerializeField] private AudioClip[] minorTracks = new AudioClip[4];
    public AudioClip[] MinorTracks
    {
        get => minorTracks;
    }

    [SerializeField] private AudioClip[] neutralTracks = new AudioClip[4];
    public AudioClip[] NeutralTracks
    {
        get => neutralTracks;
    }

    [SerializeField] private AudioClip[] majorTracks = new AudioClip[4];
    public AudioClip[] MajorTracks
    {
        get => majorTracks;
    }
}   
