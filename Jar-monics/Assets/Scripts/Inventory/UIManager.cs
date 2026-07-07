using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject seedPanel;
    [SerializeField] GameObject plantPanel;
    [SerializeField] GameObject bottlePanel;

    private static UIManager singleton;
    public static UIManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("uh oh");
            }
            return singleton;
        }
    }
    private void Awake()
    {
        if (singleton == null)
        {
            //assign
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        GameManager.Instance.screenTransition += ToggleUI;
        currentTab = FindAnyObjectByType<Tab>();
        if (currentTab != null)
        {
            currentTab.GetButton.interactable = false;
            ShowHideTabs(currentTab);
        }

    }


    void ToggleUI(Room room)
    {
        Debug.Log("room " + room);
        // seedPanel.SetActive(room.GetScreenType == Screen.GARDEN);
    }

    private Tab currentTab;
    public void ChangeTab(Tab tab)
    {
        // Debug.Log("Changing tab" + tab.GetTab);

        if (currentTab != null)
        {
            currentTab.GetButton.interactable = true;
        }
        tab.GetButton.interactable = false;
        currentTab = tab;


        ShowHideTabs(tab);
    }

    private void ShowHideTabs(Tab tab)
    {
        seedPanel.SetActive(false);
        plantPanel.SetActive(false);
        bottlePanel.SetActive(false);
        switch (tab.GetTab)
        {
            case TabCategory.SEED:
                seedPanel.SetActive(true);
                break;
            case TabCategory.PLANT:
                plantPanel.SetActive(true);
                break;
            case TabCategory.BOTTLE:
                bottlePanel.SetActive(true);
                break;
            case TabCategory.UNASSIGNED:
                Debug.LogError("Unassigned tab" + tab);
                break;
        }
    }
}

public enum TabCategory
{
    UNASSIGNED,
    SEED,
    PLANT,
    BOTTLE

}
