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

    private bool rejectPlant = false;
    public bool RejectPlant
    {
        get
        {
            return rejectPlant;
        }

        set
        {
            rejectPlant = value;
        }
    }

    private bool rejectBottle = false;
    public bool RejectBottle
    {
        get
        {
            return rejectBottle;
        }

        set
        {
            rejectBottle = value;
        }
    }
}
