
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
                wheel.wheelCollider.motorTorque = moveInput * 1000 * maxAcceleration * Time.deltaTime; // Makes bus RWD (Rear-wheel drive)
                
            }
           
            
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
                
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
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
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
