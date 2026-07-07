using UnityEngine;

public class Seed : Item
{
    private bool IsNew = true;

    private void Start()
    {
        GameManager.Instance.OnItemStartedDragging += OnStartedDragging;
        SetInfinity(true);
    }

    void OnStartedDragging(Item item)
    {
        if (item != this)
        {
            return;
        }
        // if(IsNew){
        // Instantiate(this, GameManager.Instance.GetCurrentRoom.GetPlayArea.transform);

        // }
        IsNew = false;

    }

    public override void OnItemReleased()
    {
    }
    public override void OnItemSelected()
    {
    }
}
