using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private List<Particle> particles = new List<Particle>(); 

    void Start() {
        
    }

    void FixedUpdate() {

    }

    public void Thrust() {
        particles.Add(new gameObject.AddComponent<Particle>());
    }
}

public class Particle : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        
    }

}