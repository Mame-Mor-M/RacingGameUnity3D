using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class TrafficController : MonoBehaviour
{
    public GameObject startPoint; // Starting point of the car
    private Quaternion ogRotation;
    private Vector3 ogPos;
    private float countdownTimer = 20f;
    public GameObject endPoint;   // End point of car
    public float minSpeed = 30f;    // Min speed of car
    public float maxSpeed = 60f;    // Max speed of car
    public bool loopTraffic = true; // Should traffic loop around?

    private Vector3 targetPoint; // Current target point

    void Start()
    {
        // Initialize the target point to the endPoint
        targetPoint = endPoint.transform.position;
        ogRotation = this.transform.rotation;
        ogPos = this.transform.position;
    }

    void Update()
    {
        // Move the car toward the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
        countdownTimer -= 1 * Time.deltaTime;

        if (countdownTimer <= 0) {
            this.transform.position = ogPos;
            this.transform.rotation = ogRotation;
            countdownTimer = 20f;
        }

        // Check if the car reached the target point
        if (Vector3.Distance(transform.position, endPoint.transform.position) < 0.1f)
        {
            // Teleport the car back to the startPoint
            transform.position = startPoint.transform.position;
            Debug.Log("TRANSFORM: " + ogRotation);
            this.transform.rotation = ogRotation;
            countdownTimer = 20f;
        }
    }
}
