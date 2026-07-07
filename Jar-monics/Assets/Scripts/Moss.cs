using UnityEngine;

public class Moss : MonoBehaviour
{
    [SerializeField] private Sprite[] stages = new Sprite[4];
    public Sprite[] Stages
    {
        get
        {
            return stages;
        }
    }
}