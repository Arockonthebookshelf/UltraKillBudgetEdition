using UnityEngine;
using System.Collections.Generic;

public class RocketsDropper : MonoBehaviour
{
    DropperManager dropperManager;
    PlayerInventory playerInventory;
    ParticleSystem rocketsParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        rocketsParticleSystem = GetComponent<ParticleSystem>();
        rocketsParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropRockets)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rocketsParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.rocketsMinDropAmount, dropperManager.rocketsMaxDropAmount, 1, 0) });
    }

    void Update()
    {
        var externalForces = rocketsParticleSystem.externalForces;
        externalForces.enabled = playerInventory.canPickUpRockets;
    }

    void OnParticleTrigger()
    {
        if(playerInventory.canPickUpRockets)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                playerInventory.AddRockets(dropperManager.rocketsPickupMultiplier);
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}