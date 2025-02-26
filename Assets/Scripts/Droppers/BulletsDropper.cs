using UnityEngine;
using System.Collections.Generic;

public class BulletsDropper : MonoBehaviour
{
    DropperManager dropperManager;
    PlayerInventory playerInventory;
    ParticleSystem bulletParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        bulletParticleSystem = GetComponent<ParticleSystem>();
        bulletParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropBullets)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bulletParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.bulletsMinDropAmount, dropperManager.bulletsMaxDropAmount, 1, 0) });
    }

    void OnParticleTrigger()
    {
        if(playerInventory.canPickUpBullets)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                playerInventory.currentBulletCount++;
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}