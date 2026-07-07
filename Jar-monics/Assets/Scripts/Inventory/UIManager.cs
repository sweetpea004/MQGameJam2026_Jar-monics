using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject seedPanel;
    [SerializeField] GameObject plantPanel;
    [SerializeField] GameObject bottlePanel;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private GameObject blankSpawner;
    // [SerializeField] private PlantStages[] allPlants;
    [SerializeField] private GameObject demoParentRoom;

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
        InventorySystem.Instance.UpdateInventoryUI += UpdateIcons;
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
            case ETabCategory.SEED:
                seedPanel.SetActive(true);
                break;
            case ETabCategory.PLANT:
                plantPanel.SetActive(true);
                break;
            case ETabCategory.BOTTLE:
                bottlePanel.SetActive(true);
                break;
            case ETabCategory.UNASSIGNED:
                Debug.LogError("Unassigned tab" + tab);
                break;
        }
    }

    private void UpdateIcons(ETabCategory tab, ItemElement[] array)
    {
        switch (tab)
        {
            case ETabCategory.SEED:
                return;
            case ETabCategory.PLANT:
                UpdatePlants(array);
                break;
            case ETabCategory.BOTTLE:

                break;
            case ETabCategory.UNASSIGNED:
                break;
        }
    }

    private void UpdatePlants(ItemElement[] array)
    {
        //delete everything
        for (int i = 0; i < plantPanel.transform.childCount; i++)
        {
            Transform child = plantPanel.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        foreach (ItemElement item in array)
        {
            if (item == null)
            {
                continue;
            }
            Plant plantScript = item.GetPlantItem as Plant;
            PlantStage pStage = SOManager.Instance.GetPlant(plantScript.Type);
            ItemSpawner sp = blankSpawner.GetComponent<ItemSpawner>();
            sp.SetSpawned(pStage.Prefab);
            sp.SetParent(demoParentRoom);

            //Dunno what the fuck to do here

            GameObject spawner = Instantiate(blankSpawner, transform.position, Quaternion.identity, plantPanel.transform);



        }
    }
}


