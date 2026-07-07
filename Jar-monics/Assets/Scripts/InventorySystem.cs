using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
  private static InventorySystem singleton;
  public static InventorySystem Instance
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
  [SerializeField] private ItemElement[] bottleInventory = new ItemElement[12]; //slots in inventory
  [SerializeField] private ItemElement[] plantInventory = new ItemElement[12]; //slots in inventory

  public void AddItemOne(TabCategory tab, ItemElement item)
  {
    ItemElement[] items;
    switch (tab)
    {
      case ETabCategory.PLANT:
        items = plantInventory;
        break;
      case ETabCategory.BOTTLE:
        items = plantInventory;
        break;
      default:
        return;
    }

    item.SetQuantity(item.Quantity + 1);

    if (items.Contains(item))
    {
      int index = items.FindIndex(i => i.Equals(item));
      items[index] = item;
    }
    else
    {
      items.Add(item);
    }
  }
  public void RemoveItemOne(ItemElement item)
  {
    item.SetQuantity(item.Quantity - 1);
    if (item.Quantity <= 0)
    {
      int index = items.FindIndex(i => i.Equals(item));
      items[index] = null; //remove
    }
  }
}

