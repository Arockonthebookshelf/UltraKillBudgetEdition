using UnityEngine;
using System.Collections.Generic;

public class HealthDropper : MonoBehaviour
{
    DropperManager dropperManager;
    Player player;
    ParticleSystem healthParticleSystem;
    void Awake()
    {
        dropperManager = GetComponentInParent<DropperManager>();
        player = FindFirstObjectByType<Player>();
        healthParticleSystem = GetComponent<ParticleSystem>();
        healthParticleSystem.trigger.SetCollider(0, dropperManager.playerCollider);
        if(!dropperManager.canDropHealth)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        healthParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] 
        { new ParticleSystem.Burst(0.0f, dropperManager.healthMinDropAmount, dropperManager.healthMaxDropAmount, 1, 0) });
    }

    void Update()
    {
        var externalForces = healthParticleSystem.externalForces;
        externalForces.enabled = player.canHeal;
    }

    void OnParticleTrigger()
    {
        if(player.canHeal)
        {
            List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
            int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enterParticles[i];
                p.remainingLifetime = 0;
                enterParticles[i] = p;
                player.Heal(dropperManager.healthPerPickup);
            }

            GetComponent<ParticleSystem>().SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        }
    }
}