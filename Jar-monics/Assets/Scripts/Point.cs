using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Item occupant = null;

    public Item Occupant
    {
        get
        {
            return occupant;
        }

        set
        {
            occupant = value;
        }
    }
}
