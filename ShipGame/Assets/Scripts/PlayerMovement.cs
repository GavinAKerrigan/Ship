using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;

    //Assign values here based on how we want the ship to operate. Add other variables that you believe we need for physics
    float thrustSpeed;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        thrustSpeed = 1.7f;
        rotationSpeed = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //Rocket thrusts
            Thrust();
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift))
        {
            Stablizer();
        }

        if (Input.GetKey(KeyCode.A)) {

            RotateLeft();

        }else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        autoRotationStablizer();
    }

    public void Thrust()
    {
        ship.AddRelativeForce((Vector2.up / 5) * thrustSpeed);
        //Debug.Log("Thrusting");
    }

    public void RotateLeft()
    {
        //transform.Rotate(0, 0, .2f);
        ship.angularVelocity += rotationSpeed;
        // Debug.Log("rotating left");
    }

    public void RotateRight()
    {
        ship.angularVelocity -= rotationSpeed;
        // Debug.Log("rotating right");
    }

    public void Stablizer()
    {
        ship.velocity = ship.velocity / 1.001f;
        // Debug.Log("Destablizing");
    }

    public void autoRotationStablizer()
    {
        ship.angularVelocity = ship.angularVelocity / 1.003f;
    }
}
