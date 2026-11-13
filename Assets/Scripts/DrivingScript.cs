using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelScript[] wheels;
    public float torque = 200;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 500;
    public float maxSpeed = 300;
    public Rigidbody rb;
    public float currentSpeed;


    public GameObject backLights;
    public GameObject frontLights;
    public GameObject cameraTarget;

    // Start is called before the first frame update
    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel,-1, 1);
        steer = Mathf.Clamp(steer,-1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake,0, 1) * maxBrakeTorque;

        if(brake !=0)
        {
            backLights.SetActive(true);
        }
        else
        {
            backLights.SetActive(false);
        }


        float thrustTorque = 0;
        if(currentSpeed < maxSpeed)
        {
            thrustTorque =  accel * torque;
        }

        foreach(WheelScript whell in wheels)
        {
            whell.wheelCollider.motorTorque = thrustTorque;
            if(whell.frontWheel) whell.wheelCollider.steerAngle = steer;
            else whell.wheelCollider.brakeTorque = brake;

            Quaternion quat;
            Vector3 position;

            whell.wheelCollider.GetWorldPose(out position, out quat);
            whell.wheel.transform.position = position;
            whell.wheel.transform.rotation = quat;
        }
    }

    void Start()
    {
        frontLights.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
