using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;

    //Assign values here based on how we want the ship to operate. Add other variables that you believe we need for physics
    private float thrustSpeed;
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Rocket thrusts
            Thrust();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Destablizer();
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
        ship.AddRelativeForce(Vector2.up / 2);
        //Debug.Log("Thrusting");
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, .2f);
        //Debug.Log("rotating left");
    }

    public void RotateRight()
    {
        transform.Rotate(0,0,-.2f);
        //Debug.Log("rotating right");
    }

    public void Destablizer()
    {
        ship.AddRelativeForce(Vector2.zero);
        //Debug.Log("Destablizing");
    }
}
