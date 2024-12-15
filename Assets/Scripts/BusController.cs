
using System;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public float maxAcceleration;
    private float speedFloat;
    public int speed;
    public float brakeAcceleration;


    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;
    public float timeToStop;
    [HideInInspector] public float timer;


    private Rigidbody busRb;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        busRb = GetComponent<Rigidbody>();
        busRb.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }
    public void MoveInput(float input)
    {
        moveInput = input;
    }
    public void SteerInput(float input)
    {
        steerInput = input;
    }
    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        if(moveInput != 0 && timer < timeToStop)
        {
            timer += 1 * Time.deltaTime;
        }
        else if (moveInput == 0 && timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        else if (timer > timeToStop)
        {
            timer = timeToStop;
        }
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Rear)
            {
                wheel.wheelCollider.motorTorque = moveInput * 5000 * maxAcceleration * Time.deltaTime; // Makes bus RWD (Rear-wheel drive)
                speedFloat = ((wheel.wheelCollider.rpm * 2 + MathF.PI * wheel.wheelCollider.radius) / 60.0f);
                speed = (int)(speedFloat * 3.6f);
            }
           
            
        }
    }

    void Steer()
    {
        // Calculate the desired steer angle based on input
        float _steerAngle = steerInput * turnSensitivity * maxSteerAngle;

        // Apply the calculated steer angle to the front wheels
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front && _steerAngle != 0)
            {
                // Smoothly transition to the new steer angle using Lerp
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.8f ); // Lower value for smoother transition. More drastic turns
            }
            else if (wheel.axel == Axel.Front && _steerAngle == 0)
            {
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.2f ); // Lower value for smoother transition. For when user is no longer steering
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            foreach (var wheel in wheels)
            {
                if( wheel.axel == Axel.Rear)
                {
                    wheel.wheelCollider.brakeTorque = 7000 * brakeAcceleration * Time.deltaTime;
                }
                
            }

          /*  carLights.isBackLightOn = true;
            carLights.OperateBackLights();*/
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

            /*carLights.isBackLightOn = false;
            carLights.OperateBackLights();*/
        }
    }


    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);

            // Apply the initial local rotation offset
            Quaternion localOffset = Quaternion.Euler(0, 90, 0); // Adjust as needed for your wheel's model
            wheel.wheelModel.transform.position = pos;

            // Combine the rotation from the WheelCollider with the local offset
            wheel.wheelModel.transform.rotation = rot * localOffset;
        }

    }
}
