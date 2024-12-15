using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassengerText : MonoBehaviour
{

    public BusCapacity busCapacity;
    public BusController bus;
    private int topSpeed;
    public Text passText;
    public Text gradeText;
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
        if (busCapacity.passengers / 2 >= 15)
        {
            gradeText.text = "A+";
        }

        else if (busCapacity.passengers / 2 < 15 && busCapacity.passengers / 2 > 12)
        {
            gradeText.text = "A";
        }

        else if (busCapacity.passengers / 2 < 15 && busCapacity.passengers / 2 >= 12)
        {
            gradeText.text = "B+";
        }

        else if (busCapacity.passengers / 2 < 12 && busCapacity.passengers / 2 >= 10)
        {
            gradeText.text = "B";
        }

        else if (busCapacity.passengers / 2 < 10 && busCapacity.passengers / 2 >= 8)
        {
            gradeText.text = "C+";
        }

        else if (busCapacity.passengers / 2 < 8 && busCapacity.passengers / 2 >= 6)
        {
            gradeText.text = "C";
        }
        else if (busCapacity.passengers / 2 < 6 && busCapacity.passengers / 2 >= 4)
        {
            gradeText.text = "D+";
        }

        else if (busCapacity.passengers / 2 < 4 && busCapacity.passengers / 2 >= 2)
        {
            gradeText.text = "D";
        }

        else
        {
            gradeText.text = "F";
        }

        if (topSpeed < bus.speed/50)
        {
            topSpeed = bus.speed/50 ;
        }

        if (bus.speed < 0 && SceneManager.GetActiveScene().name != "EndGrade")
        {
            speedText.text = ((bus.speed*-1)/50).ToString() + " KM/H";
        }
        else if (bus.speed > 0 && SceneManager.GetActiveScene().name != "EndGrade")
        {
            speedText.text = (bus.speed/50).ToString() + " KM/H";
        }
        else
        {
            speedText.text = topSpeed.ToString() + "KM/H";
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
