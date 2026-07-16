using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bottle : Item
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Vector3[] plantSpots = new Vector3[3];

    [SerializeField] protected Point lastPoint;

    [SerializeField] protected float lerpSpeed = 10;

    private bool[] occupancies = new bool[3];
    public int occupants = 0;

    protected new void Awake()
    {
        base.Awake();
        // type = BottleType.Clear;
        plantSpots[1] = new Vector3(0, -1.5f);


        for (int i = 0; i < occupancies.Length; i++)
        {
            occupancies[i] = false;
        }
    }

    public void AddPlant(GameObject o)
    {
        occupants++;
        for (int i = 0; i < occupancies.Length; i++)
        {
            if (occupancies[i] == false)
            {
                occupancies[i] = true;
                o.transform.parent = gameObject.transform;
                o.transform.position = plantSpots[1];
                o.transform.localScale = new Vector3(0.5f, 0.5f);
                break;
            }
        }
    }

    void PlayBottle()
    {
        foreach (Plant p in gameObject.GetComponentsInChildren<Plant>())
        {
            p.PlayMusic();
        }
    }

    void StopBottle()
    {
        foreach (Plant p in gameObject.GetComponentsInChildren<Plant>())
        {
            p.StopMusic();
        }
    }
    public new void Update()
    {
        base.Update();
        if (!isDragged && lastPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, lastPoint.transform.position, lerpSpeed * Time.deltaTime);
        }
    }
    public override void OnItemReleased()
    {
        CalculateClosestPoint();

        if (lastPoint != null)
        {
            lastPoint.Occupant = this;
            transform.localScale = lastPoint.transform.localScale * 2;
        }
    }
    public override void OnItemSelected()
    {
        if (lastPoint != null)
        {
            lastPoint.Occupant = null;
        }
    }

    private List<Point> newPoints = new List<Point>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point") && collision.gameObject.GetComponent<Point>().Occupant == null)
        {
            newPoints.Add(collision.gameObject.GetComponent<Point>());
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle") && gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            Bottle b = collision.gameObject.GetComponent<Bottle>();
            if (b.occupants < 3)
            {
                b.AddPlant(gameObject);
                lastPoint = null;
                gameObject.GetComponent<Collider2D>().enabled = false;
                isDragged = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point"))
        {
            newPoints.Remove(collision.gameObject.GetComponent<Point>());
        }
    }

    void CalculateClosestPoint()
    {
        if (newPoints.Count > 0)
        {
            newPoints = newPoints.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
            for (int i = 0; i < newPoints.Count; i++)
            {
                if (gameObject.layer == LayerMask.NameToLayer("Bottle") && !newPoints.ElementAt(i).RejectBottle)
                {
                    lastPoint = newPoints.ElementAt(i);
                    break;
                }
                else if (gameObject.layer == LayerMask.NameToLayer("Plant") && !newPoints.ElementAt(i).RejectPlant)
                {
                    lastPoint = newPoints.ElementAt(i);
                    break;
                }
            }
        }
    }
}