using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Effects : MonoBehaviour
{
    // particles
    private List<Particle> particles = new List<Particle>(); 

    [Header("Prefab Settings")]
    public Particle particlePrefab;
    [SerializeField] float particleScale = 1f;

    [Header("Spawning Settings")]
    [SerializeField] int spawnCount = 10;
    [SerializeField] float spriteSpawningDistance = 0f;

    [Header("Movement Settings")]
    [SerializeField] float spriteSpeedFactor = 1f;
    [SerializeField] float spriteRandomnessTurn = 0.5f;
    [SerializeField] float spriteRandomnessSpeed = 5f;

    [Header("Decay Settings")]
    [SerializeField] float spriteDecayRate = 0.9f;

    [Header("Color Settings")]
    [SerializeField] Color spriteColor = new Color(1f, 0.5f, 0f, 0.1f);

    void FixedUpdate()
    {
        for (int i = 0; i < particles.Count; i++) {
            if (particles[i].rb.velocity.magnitude < 0.1f) {
                Destroy(particles[i].gameObject);
                particles.RemoveAt(i);
            }
        }
    }

    public void Thrust(Vector2 thrust, float multiplier = 1f)
    { 
        for (int i = 0; i < spawnCount * multiplier; i++) CreateParticle(thrust); 
    }

    public void Stabilize(float magnitude, float multiplier = 1f)
    {
        for (int i = 0; i < spawnCount * multiplier; i++) 
        {
            float angle = UnityEngine.Random.Range(0, 360);
            Vector2 vector = new Vector2(magnitude, 0);
            vector = Quaternion.Euler(0, 0, angle) * vector;
            CreateParticle(vector, false);
        }
    }

    private void CreateParticle(Vector2 thrust, bool useSpawningDistance = true)
    {
        Particle particle = Instantiate(particlePrefab);
        particle.Initialize(spriteDecayRate, spriteColor);

        // Get the bounds of the parent sprite
        SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
        Bounds parentBounds = parentSpriteRenderer.bounds;

        // Calculate the position of the particle at the opposite edge of the parent sprite
        Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        if (useSpawningDistance) particlePosition += transform.up * spriteSpawningDistance * -1f;

        // Set the position and rotation of the particle
        particle.transform.position = particlePosition;
        particle.transform.rotation = transform.rotation;

        // Add force to the particle
        particle.rb.velocity = -1f * thrust * spriteSpeedFactor;

        // Add randomness to the particle
        AddRandomness(particle);

        // change sprite size
        particle.transform.localScale = new Vector3(particleScale, particleScale, 1f);

        // Add the particle to the list of particles
        particles.Add(particle);
    }

    private void AddRandomness(Particle particle)
    {
        float theta = UnityEngine.Random.Range(-1f * spriteRandomnessTurn, spriteRandomnessTurn);
        Vector2 oldVelocity = particle.rb.velocity;
        float cosTheta = Mathf.Cos(theta);
        float sinTheta = Mathf.Sin(theta);
        float newVelocityX = oldVelocity.x * cosTheta - oldVelocity.y * sinTheta;
        float newVelocityY = oldVelocity.x * sinTheta + oldVelocity.y * cosTheta;
        Vector2 newVelocity = new Vector2(newVelocityX, newVelocityY);

        // If the dot product of the old and new velocity is negative, reverse the new velocity
        if (Vector2.Dot(oldVelocity, newVelocity) < 0)
        {
            newVelocity = -newVelocity;
        }

        particle.rb.velocity = newVelocity;
        particle.rb.velocity += particle.rb.velocity.normalized * UnityEngine.Random.Range(0, spriteRandomnessSpeed);
    }
}