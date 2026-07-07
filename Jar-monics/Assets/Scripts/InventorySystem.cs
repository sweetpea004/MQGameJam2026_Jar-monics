using UnityEngine;
using System.Collections.Generic;
using System;

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
  public void AddItemOne(Item element)
  {
    ItemElement[] items = GetInventoryTab(element);
    ItemElement item = CreateItemElement(element);

    item.SetQuantity(item.Quantity + 1);

    if (Contains(items, item))
    {
      int index = FindIndex(items, item);
      items[index] = item;
    }
    else
    {
      items[GetFirstUnusedIndex(items)] = item;
    }
    Debug.Log(items);
  }
  public void RemoveItemOne(Item element)
  {
    ItemElement[] items = GetInventoryTab(element);
    ItemElement item = CreateItemElement(element);

    int index = FindIndex(items, item);

    item.SetQuantity(item.Quantity - 1);
    if (item.Quantity <= 0)
    {
      items[index] = null; //remove
      return;
    }

    if (Contains(items, item))
    {
      items[index] = item; //newer
    }
    else
    {
      items[GetFirstUnusedIndex(items)] = null;
    }
  }

  private ItemElement[] GetInventoryTab(Item item)
  {
    if (item is Seed)
    {
      return null;
    }
    if (item is Bottle)
    {
      return bottleInventory;
    }
    if (item is Plant)
    {
      return plantInventory;
    }
    return null;
  }

  private ItemElement CreateItemElement(Item item)
  {
    ItemElement[] items = GetInventoryTab(item);
    ItemElement element = new ItemElement(item.GetName, 0);

    if (Contains(items, element))
    {
      int index = FindIndex(items, element);
      return items[index];
    }
    else
    {
      return element;
    }
  }

  private bool Contains(ItemElement[] array, ItemElement item)
  {
    if (array == null)
    {
      return false;
    }

    for (int i = 0; i < array.Length; i++)
    {
      if (array[i] == null)
      {
        continue;
      }
      if (array[i].Equals(item))
      {
        return true;
      }
    }
    return false;
  }

  private int FindIndex(ItemElement[] array, ItemElement item)
  {
    for (int i = 0; i < array.Length; i++)
    {
      if (array[i].Equals(item))
      {
        return i;
      }
    }
    return -1;
  }
  private int GetFirstUnusedIndex(ItemElement[] array)
  {
    if (array == null)
    {
      return -1;
    }
    for (int i = 0; i < array.Length; i++)
    {
      if (array[i] == null)
      {
        return i;
      }
    }
    return -1;
  }
}


