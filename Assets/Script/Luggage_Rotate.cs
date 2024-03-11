using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage_Rotate : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the rotation speed as needed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the GameObject around its up axis (Y-axis) continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
