using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private List<Particle> particles = new List<Particle>(); 
    public Particle particlePrefab;
    public int spriteCreationDelay = 10;
    public float spriteSpawningDistance = 2f;
    private int spriteCreationDelayCounter = 0;

    void Start() {

    }

    void FixedUpdate() {
        for (int i = 0; i < particles.Count; i++) {
            if (particles[i].rb.velocity.magnitude < 0.1f) {
                Destroy(particles[i].gameObject);
                particles.RemoveAt(i);
            }
        }
    }

    public void Thrust() {
        if (spriteCreationDelayCounter == 0) {
            CreateParticle();
            spriteCreationDelayCounter = spriteCreationDelay;
        } 
        else spriteCreationDelayCounter--;
    }

    private void CreateParticle() {
        Particle particle = Instantiate(particlePrefab);
        particle.Initialize();

        // Get the bounds of the parent sprite
        SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
        Bounds parentBounds = parentSpriteRenderer.bounds;

        // Calculate the position of the particle at the opposite edge of the parent sprite
        Vector3 particlePosition = transform.position + (transform.up * spriteSpawningDistance * -1f);

        // Set the position and rotation of the particle
        particle.transform.position = particlePosition;
        particle.transform.rotation = transform.rotation;

        // Add force to the particle
        particle.rb.velocity = -1f * transform.up;

        // Add the particle to the list of particles
        particles.Add(particle);
    }
}