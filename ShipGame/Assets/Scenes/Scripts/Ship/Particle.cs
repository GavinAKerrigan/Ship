using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float decay;

    public void Initialize(float decayRate, Color color) {
        // create particle
        rb = gameObject.AddComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        decay = decayRate;
        rb.gravityScale = 0;

        // set particle graphics
        sr.color = color;
    }

    public void FixedUpdate() {
        rb.velocity *= decay;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * decay);
    }
}
