using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    BusController bus;
    // Start is called before the first frame update
    void Start()
    {
        bus = FindObjectOfType<BusController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var busModel in GameObject.FindGameObjectsWithTag("Bus"))
            {
                Destroy(busModel.gameObject);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            SceneManager.LoadScene("EndGrade");
        }
        
    }


    public void Restart()
    {
        
        SceneManager.LoadScene("StartScreen");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
