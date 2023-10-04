using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;

    //Assign values here based on how we want the ship to operate. Add other variables that you believe we need for physics
    private float thrustSpeed;
    [SerializeField] float rotationSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //Rocket thrusts
            Thrust();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Destablizer();
            StablizeThrust();
        }

        if (Input.GetKey(KeyCode.A)) {

            RotateLeft();

        } else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        StablizeRotation();
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
        //transform.Rotate(0, 0, .2f);
        ship.angularVelocity += rotationSpeed;
        // Debug.Log("rotating left");
    }

    public void RotateRight()
    {
        transform.Rotate(0,0,-.2f);
        //Debug.Log("rotating right");
    }

    public void Destablizer()
    {
        ship.angularVelocity -= rotationSpeed;
        // Debug.Log("rotating right");
    }

    public void StablizeThrust()
    {
        ship.AddRelativeForce(Vector2.zero);
        //Debug.Log("Destablizing");
    }

    public void StablizeRotation()
    {
        ship.angularVelocity = ship.angularVelocity / 1.003f;
    }
}
