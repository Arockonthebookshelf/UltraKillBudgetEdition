using UnityEngine;
using System.Collections.Generic;

public class BulletsDropper : MonoBehaviour
{
    DropperManager dropperManager;
    ParticleSystem bulletParticleSystem;
    int particleCollisionCount;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        bulletParticleSystem = GetComponent<ParticleSystem>();
        bulletParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
    }

    void Start()
    {
        bulletParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.bulletsMinDropAmount, dropperManager.bulletsMaxDropAmount, 1, 0) });
    }

    void Update()
    {
        // check if player can pick up bullets
    }

    void OnParticleTrigger()
    {
        // if player can pick up bullets only then continue
        List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
        int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enterParticles[i];
            p.remainingLifetime = 0;
            enterParticles[i] = p;
            particleCollisionCount++;
            Debug.Log("Particle " + i + " destroyed. Total collisions: " + particleCollisionCount);
        }

        GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        Debug.Log("Particles collided with the player: " + particleCollisionCount);
    }
}

