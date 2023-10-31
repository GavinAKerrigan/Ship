using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // object reference
    private Rigidbody2D rb;
    SpriteRenderer sr;

    // controls references
    private List<KeyCode> rotateLeftKeys, rotateRightKeys, stabilizeKeys, thrustKeys;

    // serialized ship stats
    [Header("Input Fields")]
    [SerializeField] float  thrust;
    [SerializeField] float  rotation;
    [SerializeField] float  stablizer;

    [Header("Physics Fields")]
    [SerializeField] float  drag;
    [SerializeField] float  angularDrag;
    [SerializeField] float  maxVelocity;
    [SerializeField] float  maxAngularVelocity;

    // internal ship stats
    private float   iThrust,                // adjusted thrusting power
                    iRotation,              // adjusted turning power
                    iStablizer,             // adjusted stabilizing power
                    iMaxVelocity,           // adjusted maximum velocity
                    iMaxAngularVelocity,    // adjusted maximum turning speed
                    iDrag,                  // adjusted natural slowdown (thrusting)
                    iAngularDrag,           // adjusted natural slowdown (turning)
                    iLastThrust;            // last thrusting power

    private bool stablizerActive;

    void Awake()
    {
        // get object references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        stablizerActive = false;

        // set up control scheme
        thrustKeys = new List<KeyCode> { KeyCode.W, KeyCode.I, KeyCode.UpArrow, KeyCode.Space };
        rotateLeftKeys = new List<KeyCode> { KeyCode.A, KeyCode.J, KeyCode.LeftArrow };
        rotateRightKeys = new List<KeyCode> { KeyCode.D, KeyCode.L, KeyCode.RightArrow };
        stabilizeKeys = new List<KeyCode> { KeyCode.S, KeyCode.K, KeyCode.DownArrow };

        FixStats();

        Debug.Log(iThrust);

        // apply drag
        rb.drag = iDrag;
        rb.angularDrag = iAngularDrag;
    }

    void FixedUpdate()
    {
        Move();
        LimitVelocity();
        LimitAngularVelocity();
        FuelReset();
    }

    // modify ship stats to reflect real values
    private void FixStats() 
    {
        iThrust = thrust * 50f;
        iRotation = rotation * 1.5f;
        iStablizer = 1 - stablizer * 0.01f;
        iMaxVelocity = maxVelocity * 5000f;
        iMaxAngularVelocity = maxAngularVelocity * 100f;
        iDrag = drag * 0.1f;
        iAngularDrag = angularDrag * 0.3f;
    }

    private void LimitVelocity() { rb.velocity = Vector2.ClampMagnitude(rb.velocity, iMaxVelocity); }
    private void LimitAngularVelocity() { rb.angularVelocity = Math.Clamp(rb.angularVelocity, -iMaxAngularVelocity, iMaxAngularVelocity); }

    /// interperates the controls and calls the appropriate functions
    private void Move()
    {
        bool thrusting = false;
        foreach (KeyCode key in thrustKeys) if (Input.GetKey(key)) { thrusting = true; break; }
        foreach (KeyCode key in rotateLeftKeys) if (Input.GetKey(key)) { rb.angularVelocity += iRotation; break; }
        foreach (KeyCode key in rotateRightKeys) if (Input.GetKey(key)) { rb.angularVelocity -= iRotation; break; }
        foreach (KeyCode key in stabilizeKeys) if (Input.GetKey(key))
            {
                rb.velocity *= iStablizer;
                rb.angularVelocity *= iStablizer;
                break;
            }

        // apply thrust
        if (thrusting)
        {
            rb.AddRelativeForce(Vector2.up * iThrust * Time.deltaTime);
            iLastThrust = iThrust;
            Fuel.f.FuelDecreaser();
        }
    }

    public void FuelReset()
    {
        if (Fuel.f.fuel <= 0f)
        {
            sr.color = Color.red;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Fuel.f.fuel = 100f;
        }
    }

    // collision handler
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Victory") SceneManager.LoadScene(sceneName: "Menu");
        else if (collision.gameObject.tag != "Respawn") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}