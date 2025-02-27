using UnityEngine;
using System.Collections.Generic;

public class CapacitorsDropper : MonoBehaviour
{
    DropperManager dropperManager;
    PlayerInventory playerInventory;
    ParticleSystem capacitorParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        capacitorParticleSystem = GetComponent<ParticleSystem>();
        capacitorParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropCapacitors)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        capacitorParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.capacitorsMinDropAmount, dropperManager.capacitorsMaxDropAmount, 1, 0) });
    }

    void OnParticleTrigger()
    {
        if(playerInventory.canPickUpCapacitors)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                playerInventory.currentCapacitorCount++;
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}