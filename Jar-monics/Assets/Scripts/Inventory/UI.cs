using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject seedPanel;


    void Start(){
        GameManager.Instance.screenTransition += ToggleUI;
        seedPanel.SetActive(false);
    }
    

    void ToggleUI(Room room){
        Debug.Log("room " + room);
        seedPanel.SetActive(room.GetScreenType == Screen.GARDEN);
    }
}
