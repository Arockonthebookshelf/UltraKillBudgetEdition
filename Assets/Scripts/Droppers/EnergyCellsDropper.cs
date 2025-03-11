using UnityEngine;
using System.Collections.Generic;

public class EnergyCellsDropper : MonoBehaviour
{
    DropperManager dropperManager;
    PlayerInventory playerInventory;
    ParticleSystem energyCellsParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        energyCellsParticleSystem = GetComponent<ParticleSystem>();
        energyCellsParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropEnergyCells)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        energyCellsParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.energyCellsMinDropAmount, dropperManager.energyCellsMaxDropAmount, 1, 0) });
    }

    void Update()
    {
        var externalForces = energyCellsParticleSystem.externalForces;
        externalForces.enabled = playerInventory.canPickUpEnergyCells;
    }

    void OnParticleTrigger()
    {
        if(playerInventory.canPickUpEnergyCells)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                playerInventory.AddEnergyCells(dropperManager.energyCellsPickupMultiplier);
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}