using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorLift : MonoBehaviour
{

    public bool canMove;

    [SerializeField] float speed;
    [SerializeField] int startPoint;
    public GameObject gate;
    [SerializeField] Transform[] points;
    private float delay = 4;



    int i;
    bool reverse;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
            canMove = false;
            

            if ( i == points.Length - 1)
            {
                reverse = true;
                
                i--;
                return;
            }
            else if(i == 0)
            {
                reverse = true;
                i++;
                return;
            }

            if (reverse)
            {
                i++;
            }
            else
            {
                i++;
            }
        }delay -= 1 * Time.deltaTime;
        if (canMove){
            delay -= 1 * Time.deltaTime;
            if ( delay <= 0)
            {
                gate.SetActive(true);
                transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            }
            
        }
        if (transform.position.y <= 0)
        {
            gate.SetActive(false);
        }
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            
            canMove = true;
        }
        else
        {
            delay = 3;
        }
    }
}
