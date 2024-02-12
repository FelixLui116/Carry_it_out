using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    // public float speed = 2.0f;

    // void Update()
    // {
    //     MoveConveyorBelt();
    // }

    // void MoveConveyorBelt()
    //  {
    //     float offset = Time.time * speed;
    //     // GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);

    //     // Move child objects on the conveyor belt using raycasting
    //     foreach (Transform child in transform)
    //     {
    //         // if (child.CompareTag("Plan"))
    //         // {
    //             Ray ray = new Ray(child.position, transform.right);
    //             RaycastHit hit;

    //             if (Physics.Raycast(ray, out hit, speed * Time.deltaTime))
    //             {
    //                 child.position += transform.right * speed * Time.deltaTime;
    //             }
    //         // }
    //     }
    // }
    
    public float speed = 2.0f;
    public Vector3 direction;
    public List<GameObject> onBelt;
    void Update()
    {
        for(int i = 0; i <= onBelt.Count -1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
