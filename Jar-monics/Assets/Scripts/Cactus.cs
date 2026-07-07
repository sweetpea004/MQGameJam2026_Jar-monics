using UnityEditor.SceneManagement;
using UnityEngine;

public class Cactus : MonoBehaviour
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