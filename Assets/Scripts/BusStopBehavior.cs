using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BusStopBehavior : MonoBehaviour
{
    public List<GameObject> commuters = new List<GameObject>();
    private List<GameObject> commuterMeshes = new List<GameObject>();
    public PickupBehavior pickupBehavior;
    


    [SerializeField] GameObject passPrefab;
    [SerializeField] GameObject lineStart;
    private int gap = 0;

    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        foreach(var passenger in commuters)
        {
            passPrefab.name = "Passenger " + i;
            GameObject newPass = Instantiate(passPrefab, new Vector3(lineStart.transform.position.x, lineStart.transform.position.y, lineStart.transform.position.z + gap), Quaternion.identity);
            newPass.transform.SetParent(this.gameObject.transform);
            gap +=2;
            i++;
            //commuters = 
        }
        i = 0;
        commuters = new List<GameObject>();
        foreach (var passenger in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (passenger.CompareTag("Passenger"))
            {
                commuters.Add(passenger.gameObject);
            }
        }
        gap = 3;

        i = 0;
        pickupBehavior = FindObjectOfType<PickupBehavior>();
        InvokeRepeating("BoardBus", 2.0f, 0.4f);
    }

    // Update is called once per frame
    void BoardBus()
    {
        if (pickupBehavior.stopped && this.i < commuters.Count && pickupBehavior.zoneName == this.gameObject.name)
        {
            commuters[i].transform.Translate(0, 0, - gap);
            i++;
        }
        else if(i >= commuters.Count) {

            i = 0;
        }
    }

}
