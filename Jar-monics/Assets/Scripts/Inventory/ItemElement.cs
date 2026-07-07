using UnityEngine;

public class ItemElement
{
    private readonly string itemName;
    private int quantity;
    private Item item;

    public Item GetPlantItem
    {
        get => item;
    }
    public ItemElement(string name, int qty, Item itemScript)
    {
        itemName = name;
        quantity = qty;
        item = itemScript;
    }
    public int Quantity
    {
        get => quantity;
    }
    public void SetQuantity(int value)
    {
        quantity = value;
    }
    public string Name
    {
        get => itemName;
    }


    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        ItemElement other = obj as ItemElement;
        if (other == null)
        {
            return false;
        }
        return other.Name.Equals(this.Name);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return string.Format("{0}: {1}", Name, quantity);
    }
}
