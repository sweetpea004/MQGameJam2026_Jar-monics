using UnityEditor.SceneManagement;
using UnityEngine;

public class Moss : MonoBehaviour
{
    [SerializeField] private Sprite[] stages = new Sprite[3];
    public Sprite[] Stages
    {
        get
        {
            return stages;
        }
    }
}