using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraProperties(GameObject car)
    {
        cam.Follow = car.GetComponent<DrivingScript>().rb.transform;
        cam.Follow = car.GetComponent<DrivingScript>().cameraTarget.transform;
    }
}
