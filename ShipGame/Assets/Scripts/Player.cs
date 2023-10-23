using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // object reference
    private Rigidbody2D ship;
    SpriteRenderer spriteRenderer;

    // controls references
    private List<KeyCode> rotateLeftKeys, rotateRightKeys, stabilizeKeys, thrustKeys;

    // serialized ship stats
    [SerializeField]
    float thrust,             // thrusting power
                            rotation,           // turning power
                            stablizer,          // stabilizing power
                            maxVelocity,        // maximum velocity
                            maxAngularVelocity, // maximum turning speed
                            drag,               // natural slowdown (thrusting)
                            angularDrag,        // natural slowdown (turning)
                            afterburn;          // afterburner strength (latent thrust)
    // internal ship stats
    private float iThrust,                // adjusted thrusting power
                    iRotation,              // adjusted turning power
                    iStablizer,             // adjusted stabilizing power
                    iMaxVelocity,           // adjusted maximum velocity
                    iMaxAngularVelocity,    // adjusted maximum turning speed
                    iDrag,                  // adjusted natural slowdown (thrusting)
                    iAngularDrag,           // adjusted natural slowdown (turning)
                    iLastThrust,            // last thrusting power
                    iAfterburn;             // adjusted afterburner strength (latent thrust)

    private bool stablizerActive;

    // Called before the first frame update
    void Awake()
    {
        // get object references
        ship = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stablizerActive = false;

        // set up control scheme
        thrustKeys = new List<KeyCode> { KeyCode.W, KeyCode.I, KeyCode.UpArrow, KeyCode.Space };
        rotateLeftKeys = new List<KeyCode> { KeyCode.A, KeyCode.J, KeyCode.LeftArrow };
        rotateRightKeys = new List<KeyCode> { KeyCode.D, KeyCode.L, KeyCode.RightArrow };
        stabilizeKeys = new List<KeyCode> { KeyCode.S, KeyCode.K, KeyCode.DownArrow };

        // modify ship stats to reflect real values
        iThrust = thrust * 3f;
        iRotation = rotation * 1.5f;
        iStablizer = 1 - stablizer * 0.008f;
        iMaxVelocity = maxVelocity * 40f;
        iMaxAngularVelocity = maxAngularVelocity * 500f;
        iDrag = drag * 3.2f;
        iAngularDrag = angularDrag * 3.2f;
        iAfterburn = 1 - afterburn * 0.032f;

        Debug.Log(iThrust);

        // apply drag
        ship.drag = iDrag;
        ship.angularDrag = iAngularDrag;
    }

    // Called once per frame
    void Update()
    {
        Move();
        LimitVelocity();
        LimitAngularVelocity();
        fuelReset();
    }

    // Called once per frame during Update()
    private void LimitVelocity() { ship.velocity = Vector2.ClampMagnitude(ship.velocity, iMaxVelocity * Time.deltaTime); }
    private void LimitAngularVelocity() { ship.angularVelocity = Math.Clamp(ship.angularVelocity, -iMaxAngularVelocity * Time.deltaTime, iMaxAngularVelocity * Time.deltaTime); }

    /// interperates the controls and calls the appropriate functions
    private void Move()
    {
        bool thrusting = false;
        foreach (KeyCode key in thrustKeys) if (Input.GetKey(key)) { thrusting = true; break; }
        foreach (KeyCode key in rotateLeftKeys) if (Input.GetKey(key)) { ship.angularVelocity += iRotation * Time.deltaTime; break; }
        foreach (KeyCode key in rotateRightKeys) if (Input.GetKey(key)) { ship.angularVelocity -= iRotation * Time.deltaTime; break; }
        foreach (KeyCode key in stabilizeKeys) if (Input.GetKey(key))
            {
                ship.velocity *= iStablizer * Time.deltaTime;
                ship.angularVelocity *= iStablizer * Time.deltaTime;
                break;
            }

        // apply thrust or afterburner
        if (thrusting) { ship.AddRelativeForce(Vector2.up * iThrust * Time.deltaTime); iLastThrust = iThrust * Time.deltaTime; Fuel.f.fuelDecreaser(); } // default thrust
        else
        {
            ship.AddRelativeForce(Vector2.up * iLastThrust * Time.deltaTime);
            iLastThrust *= iAfterburn * Time.deltaTime;  // afterburner
        }
    }

    public void fuelReset()
    {
        if (Fuel.f.fuel <= 0f)
        {
            spriteRenderer.color = Color.red;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Fuel.f.fuel = 100f;
        }
    }

    // collision handler
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Victory")
        {
            spriteRenderer.color = Color.green;
            SceneManager.LoadScene(sceneName: "Menu");
        }
        else if (collision.gameObject.tag != "Respawn")
        {
            spriteRenderer.color = Color.red;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}