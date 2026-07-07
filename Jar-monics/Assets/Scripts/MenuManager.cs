using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject creditPage;
    [SerializeField] private Button credits;
    [SerializeField] private Button quit;
    [SerializeField] private Button back;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        Button creditsButton = credits.GetComponent<Button>();
        creditsButton.onClick.AddListener(OnOpenCredits);

        Button quitButton = quit.GetComponent<Button>();
        quitButton.onClick.AddListener(OnQuit);

        Button backButton = back.GetComponent<Button>();
        backButton.onClick.AddListener(OnBack);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOpenCredits()
    {
        Debug.Log("Openned creditssss");
        creditPage.SetActive(true);
    }

    void OnQuit()
    {
        Application.Quit();
    }

    void OnBack()
    {
        Debug.Log("closeddd creditssss");
        creditPage.SetActive(false);
    } 
}
