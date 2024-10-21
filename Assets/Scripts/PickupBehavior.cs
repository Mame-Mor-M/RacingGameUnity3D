using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{

    public BusController bus;
    [HideInInspector] public bool stopped;
    private bool inPickupZone;
    private BusCapacity capacity;

    [HideInInspector]public string zoneName;

    // Start is called before the first frame update
    void Start()
    {
        capacity = GetComponent<BusCapacity>();
    }

    // Update is called once per frame
    void Update()
    {
       if (inPickupZone)
        {
            PickingUp();
        }
        //Debug.Log(bus.timer);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickupZone")
        {
            inPickupZone = true;
            zoneName = other.gameObject.transform.parent.name;
            Debug.Log("IN PICKUP ZONE ' " + zoneName + "'");
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "PickupZone")
        {
            inPickupZone = false;
            Debug.Log("LEFT PICKUP ZONE");
        }
    }




    public void PickingUp()
    {
        if (inPickupZone)
        {
            if(bus.timer <= 0)
            {
                stopped = true;
                Debug.Log("CAN PICK UP");
            }
            else
            {
                stopped = false;
            }
        }
    }
}
