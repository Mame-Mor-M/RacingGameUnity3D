using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject startPoint; // Starting point of the car
    public GameObject endPoint;   // End point of car
    public float minSpeed = 30f;    // Min speed of car
    public float maxSpeed = 60f;    // Max speed of car
    public bool loopTraffic = true; // Should traffic loop around?

    private Vector3 targetPoint; // Current target point

    void Start()
    {
        // Initialize the target point to the endPoint
        targetPoint = endPoint.transform.position;
    }

    void Update()
    {
        // Move the car toward the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
        

        // Check if the car reached the target point
        if (Vector3.Distance(transform.position, endPoint.transform.position) < 0.1f)
        {
            // Teleport the car back to the startPoint
            transform.position = startPoint.transform.position;
        }
    }
}
