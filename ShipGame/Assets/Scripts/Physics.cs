using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    //Fields
    [SerializeField]
    float maxVelocity;
    [SerializeField]
    float maxAngularVelocity;
    Rigidbody2D rb;

    //Vector2 acceleration;
    //Vector2 lastVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MaxVelocity();
        MaxAngularVelocity();
    }

    private void MaxVelocity()
    {
        if (rb.velocity.sqrMagnitude > maxVelocity)
        {
            rb.velocity *= 0.99f;
        }
    }

    private void MaxAngularVelocity()
    {
        if (rb.angularVelocity < -maxAngularVelocity) { 
            rb.angularVelocity = -maxAngularVelocity; 
        }
        if (rb.angularVelocity > maxAngularVelocity) { 
            rb.angularVelocity = maxAngularVelocity; 
        }
    }

    /*private Vector2 GetAcceleration()
    {
        acceleration = (rb.velocity-lastVelocity)/Time.deltaTime;
        lastVelocity = rb.velocity;
        return acceleration;
    }*/
}