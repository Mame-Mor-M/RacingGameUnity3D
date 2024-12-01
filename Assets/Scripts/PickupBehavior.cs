using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{

    public BusController bus;
    [HideInInspector] public bool stopped;
    [HideInInspector] public bool inPickupZone;
    
    private BusCapacity capacity;

    [HideInInspector]public string zoneName;

    public CamController cam;
    public float pickupOffset;
    private float baseOffset;
    // Start is called before the first frame update
    void Start()
    {
        capacity = GetComponent<BusCapacity>();
        baseOffset = cam.moveOffset.x;
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

        if (other.gameObject.tag == "StopBarriers")
        {
            Debug.Log("BRAAAAAAKES!!! CAN'T HIT PASSENGERS, YOU'LL GET FIRED!");
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
                cam.moveOffset.x = pickupOffset;
                stopped = true;
                Debug.Log("CAN PICK UP");
            }
            else
            {
                cam.moveOffset.x = baseOffset;
                stopped = false;
            }
        }
    }
}
