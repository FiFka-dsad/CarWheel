using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame

    public float speed;
    void Update()
    {
        transform.Rotate(0, -speed * Time.deltaTime, 0);
    }
}