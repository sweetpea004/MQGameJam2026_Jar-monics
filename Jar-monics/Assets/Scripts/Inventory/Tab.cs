using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Tab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ETabCategory tab;
    public ETabCategory GetTab
    {
        get => tab;
    }
    private Button button;
    public Button GetButton
    {
        get => button;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void OnPointerClick(PointerEventData pointer)
    {
        UIManager.Instance.ChangeTab(this);
    }
}
