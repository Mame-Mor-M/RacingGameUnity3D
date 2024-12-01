using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassengerText : MonoBehaviour
{

    public BusCapacity busCapacity;
    public BusController bus;
    public Text passText;
    public Text speedText;
    // Start is called before the first frame update
    void Start()
    {
        busCapacity = FindObjectOfType<BusCapacity>();
        bus = FindObjectOfType<BusController>();
    }

    // Update is called once per frame
    void Update()
    {
        passText.text = ("Passengers: " + busCapacity.passengers/2).ToString();
        
        if(bus.speed < 0)
        {
            speedText.text = (bus.speed*-1).ToString() + " KM/H";
        }
        else
        {
            speedText.text = bus.speed.ToString() + " KM/H";
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
