using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusCapacity : MonoBehaviour
{
    public int capacity = 0;
    public int addedFromStop = 0;
    public BusStopBehavior stopBehavior;
    public List<GameObject> busStops = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var stop in GameObject.FindGameObjectsWithTag("BusStop")) {
            busStops.Add(stop);
        }
        InvokeRepeating("DisplayCapacity", 2.0f, 1.0f);
    }


    void DisplayCapacity()
    {
        Debug.Log("Bus Capacity: " + capacity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Passenger")
        {
            stopBehavior.commuters.Remove(other.gameObject);
            other.GetComponent<MeshRenderer>().enabled = false;

            capacity += 1/2;
        }
        
    }
}
