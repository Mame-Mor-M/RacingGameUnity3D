using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform carTarget;
    private bool isFreeRotating;


    public float rotationSpeed = 100f;
    public GameObject rotationPoint;
    public float shakeIntensity = 0.01f; // Default intensity of screen shake
    void FixedUpdate()
    {

        if (Input.GetMouseButton(2)) // Middle mouse button pressed
        {
            isFreeRotating = true;
            HandleManualRotation();
            HandleMovement();
        }
        else
        {
            isFreeRotating = false;
            FollowTarget();
        }

        

    }

    void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
        if (targetPos.x - transform.position.x > 7f || targetPos.z - transform.position.z > 7f || targetPos.z - transform.position.z > 7f) // Ensures camera only shakes after a distance of 7 is reached across any axis
        {
            AddScreenShake(targetPos);
        }
        
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }


    void AddScreenShake(Vector3 targetPos)
    {
        // Calculate the distance between the camera and the target
        float distance = Vector3.Distance(transform.position, carTarget.position);

        // Calculate shake magnitude based on distance
        float currentShakeIntensity = shakeIntensity;

        Vector3 shakeOffset = new Vector3(
            Random.Range(-currentShakeIntensity, currentShakeIntensity),
            Random.Range(-currentShakeIntensity, currentShakeIntensity),
            Random.Range(-currentShakeIntensity, currentShakeIntensity)
        );
        transform.position += shakeOffset;
    }

    void HandleManualRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the camera around its local axes
        transform.Rotate(Vector3.up, mouseX, Space.World);       // Horizontal rotation
        transform.Rotate(Vector3.right, -mouseY, Space.Self);   // Vertical rotation
    }

}



