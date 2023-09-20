using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }

        if (Input.GetKey(KeyCode.A)) {

            RotateLeft();

        } else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
    }

    public void Thrust()
    {
        //ship.AddRelativeForce()
            //Rocket thrusts
            Debug.Log("Thrusting");
    
    }

    public void RotateLeft()
    {/*
        if (Input.GetKey("a"))
        {
            //ship rotates left
        }   else if (Input.GetKey("left"))
        {
            //ship rotates left
        }

        if (Input.GetKey("d"))
        {
            //ship rotates right

        }   else if (Input.GetKey("right"))
        {
            //ship rotates right
        }
        */

        Debug.Log("rotating left");
    }

    public void RotateRight()
    {
        //ship rotates right
        Debug.Log("rotating right");
    }
}
