using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public void Initialize() {
        // create particle
        rb = gameObject.AddComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb.gravityScale = 0;

        // set particle graphics
        spriteRenderer.color = new Color(1f, 0.5f, 0f, 0.1f);
    }

    public void FixedUpdate() {
        rb.velocity *= 0.99f;
    }
}
