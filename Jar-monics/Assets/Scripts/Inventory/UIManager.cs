using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject seedPanel;
    [SerializeField] GameObject playSpace;

        private static UIManager singleton;
    public static UIManager Instance{
        get{
        if(singleton == null){
            Debug.LogError("uh oh");
        }
        return singleton;
        }
    }

    void Start(){
        GameManager.Instance.screenTransition += ToggleUI;
        seedPanel.SetActive(false);
    }
    

    void ToggleUI(Room room){
        Debug.Log("room " + room);
        seedPanel.SetActive(room.GetScreenType == Screen.GARDEN);
    }
}
