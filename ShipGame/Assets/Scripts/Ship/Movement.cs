using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Properties
    private Rigidbody2D rb;
    private Effects effects;
    SpriteRenderer sr;

    [Header("Input")]
    [SerializeField] float  thrust = 5f;
    [SerializeField] float  rotation = 5f;
    [SerializeField] float  stabilizer = 5f;
    private float iThrust, iRotation, iStabilizer;

    [Header("Physics")]
    [SerializeField] float  drag = 5f;
    [SerializeField] float  angularDrag = 5f;
    [SerializeField] float  maxVelocity = 5f;
    [SerializeField] float  maxAngularVelocity = 5f;
    private float iDrag, iAngularDrag, iMaxVelocity, iMaxAngularVelocity;
    public bool paused = false;

    [Header("Fuel")]
    [SerializeField] float fuel;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float thrustCost = 1f;         // cost per second
    [SerializeField] float stabilizeCost = 1f;      // "
    [SerializeField] TextMeshProUGUI fuelDisplay;

    [Header("Loading")]
    [SerializeField] string nextScene = "Level Select";

    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Awake Functions
    void Awake() 
    {
        // get object references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        effects = GetComponent<Effects>();

        FixStats();
    }

    private void FixStats() 
    {
        // translate given stats to internal stats
        iThrust = thrust * 50f;
        iRotation = rotation * 100f;
        iStabilizer = stabilizer * 0.75f;
        iMaxVelocity = maxVelocity * 5000f;
        iMaxAngularVelocity = maxAngularVelocity * 1000f;
        iDrag = drag * 0.1f;
        iAngularDrag = angularDrag * 0.3f;

        // apply drag
        rb.angularDrag = iAngularDrag;
        rb.drag = iDrag;

        // set up fuel
        fuel = maxFuel;
    }



    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Update Functions
    void FixedUpdate() 
    {
        if (paused) return;
        Move();
        CheckFuel();
    }

    private void CheckFuel() 
    {
        // update fuel display
        int percent = (int)(fuel / maxFuel * 100);
        fuelDisplay.text = "Fuel: " + percent + "%";
        fuelDisplay.color = Color.Lerp(Color.red, Color.green, percent / 100f);

        // check for out of fuel
        if (fuel <= 0) Reload();
    }

    private void Move() 
    {
        Rotate();
        if (Input.GetAxisRaw("Vertical") > 0) Thrust();
        else if (Input.GetAxisRaw("Vertical") < 0) Stabilize();
        LimitVelocity();
        LimitAngularVelocity();
    }



    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Movement Functions
    private void LimitVelocity() 
    {
        // limit the velocity of the rigidbody to the internal max velocity
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, iMaxVelocity);
    }

    private void LimitAngularVelocity() 
    {
        // limit the angular velocity of the rigidbody to the internal max velocity
        rb.angularVelocity = Math.Clamp(rb.angularVelocity, -iMaxAngularVelocity, iMaxAngularVelocity);
    }

    private void Rotate() 
    {
        // apply rotation to the rigidbody based on horizontal input axis
        rb.angularVelocity += Input.GetAxisRaw("Horizontal") * iRotation * Time.deltaTime * -1f;
    }

    private void Stabilize() 
    {
        // stabilize the rigidbody's velocity and angular velocity
        rb.velocity -= rb.velocity.normalized * iStabilizer * Time.deltaTime;
        rb.angularVelocity -= rb.angularVelocity * iStabilizer * Time.deltaTime;

        // if the rigidbody is close enough to 0, set it to 0 to avoid jittering 
        if (rb.velocity.magnitude < 0.01f) rb.velocity = Vector2.zero;
        if (rb.angularVelocity < 0.01f && rb.angularVelocity > -0.01f) rb.angularVelocity = 0;

        // add effects
        effects.Stabilize(rb.velocity.magnitude + 1);

        // apply fuel cost
        fuel -= stabilizeCost * Time.deltaTime;
    }

    private void Thrust() 
    {
        // apply force to the rigidbody based on vertical input axis
        rb.AddRelativeForce(Vector2.up * iThrust * Time.deltaTime);

        // add effects
        effects.Thrust(transform.up * iThrust * Time.deltaTime);

        // apply fuel cost
        fuel -= thrustCost * Time.deltaTime;
    }
    


    //  -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   Collision Functions
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if (paused) return;
        if (collision.gameObject.tag == "Victory") SceneManager.LoadScene(nextScene);
        else if (collision.gameObject.tag != "Respawn") Reload();
    }

    private void Reload() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}