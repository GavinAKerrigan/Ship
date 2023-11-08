using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // object references
    private Rigidbody2D rb;
    private Effects effects;
    SpriteRenderer sr;

    [Header("Input Fields")]
    [SerializeField] float  thrust = 5f;
    [SerializeField] float  rotation = 5f;
    [SerializeField] float  stabilizer = 5f;
    private float iThrust, iRotation, iStabilizer;

    [Header("Physics Fields")]
    [SerializeField] float  drag = 5f;
    [SerializeField] float  angularDrag = 5f;
    [SerializeField] float  maxVelocity = 5f;
    [SerializeField] float  maxAngularVelocity = 5f;
    private float iDrag, iAngularDrag, iMaxVelocity, iMaxAngularVelocity;

    [Header("Fuel Fields")]
    [SerializeField] float fuel;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float thrustCost = 1f;         // cost per second
    [SerializeField] float stabilizeCost = 1f;      // "
    [SerializeField] TextMeshProUGUI fuelDisplay;

    [Header("Loading Fields")]
    [SerializeField] string nextScene = "Level Select";

    void Awake()
    {
        // get object references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // set up ship stats
        FixStats();

        // set up effects
        effects = GetComponent<Effects>();
    }

    void FixedUpdate()
    {
        Move();
        LimitVelocity();
        LimitAngularVelocity();
        CheckFuel();
    }

    // modify ship stats to reflect real values
    private void FixStats() 
    {
        // fix stats
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

    private void LimitVelocity() { rb.velocity = Vector2.ClampMagnitude(rb.velocity, iMaxVelocity); }
    private void LimitAngularVelocity() { rb.angularVelocity = Math.Clamp(rb.angularVelocity, -iMaxAngularVelocity, iMaxAngularVelocity); }

    /// interperates the controls and calls the appropriate functions
    private void Move()
    {
        // rotation (inverted for clockwise positive motion)
        rb.angularVelocity += Input.GetAxisRaw("Horizontal") * iRotation * Time.deltaTime * -1f;

        // get raw input
        float rawV = Input.GetAxisRaw("Vertical");

        if (rawV > 0)
        {
            // thrust
            rb.AddRelativeForce(Vector2.up * rawV * iThrust * Time.deltaTime);

            // effects
            effects.Thrust(transform.up * rawV * iThrust * Time.deltaTime);

            // subtract fuel
            fuel -= thrustCost * Time.deltaTime;
        }

        else if (rawV < 0)
        {
            // stabilize
            rb.velocity -= rb.velocity.normalized * iStabilizer * Time.deltaTime;
            rb.angularVelocity -= rb.angularVelocity * iStabilizer * Time.deltaTime;

            effects.Stabilize(rb.velocity.magnitude + 1);

            // subtract fuel
            fuel -= stabilizeCost * Time.deltaTime;
        }
    }

    // checks fuel and updates the fuel display
    private void CheckFuel()
    {
        int percent = (int)(fuel / maxFuel * 100);
        fuelDisplay.text = "Fuel: " + percent + "%";
        fuelDisplay.color = Color.Lerp(Color.red, Color.green, percent / 100f);

        if (fuel <= 0) Reload();
    }

    // collision handler
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Victory") SceneManager.LoadScene(nextScene);
        else if (collision.gameObject.tag != "Respawn") Reload();
    }

    // loads a given scene, or the current scene if not given a scene name
    private void Reload() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}