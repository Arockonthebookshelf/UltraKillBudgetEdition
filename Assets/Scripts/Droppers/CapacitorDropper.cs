using UnityEngine;
using System.Collections.Generic;

public class ShotgunAmmoDropper : MonoBehaviour
{
    DropperManager dropperManager;
    
    ParticleSystem capacitorParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        
        capacitorParticleSystem = GetComponent<ParticleSystem>();
        capacitorParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropShotgunAmmo)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        var externalForces = capacitorParticleSystem.externalForces;
        externalForces.enabled = PlayerInventory.instance.canPickUpShotgunAmmo;
    }

    void Start()
    {
        capacitorParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.shotgunAmmoMinDropAmount, dropperManager.shotgunAmmoMaxDropAmount, 1, 0) });
    }

    void OnParticleTrigger()
    {
        if(PlayerInventory.instance.canPickUpShotgunAmmo)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                PlayerInventory.instance.AddShotgunAmmo(dropperManager.shotgunAmmoPickupMultiplier);
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}