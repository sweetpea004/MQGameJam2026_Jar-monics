using UnityEditor.SceneManagement;
using UnityEngine;

public class Succulent : MonoBehaviour
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