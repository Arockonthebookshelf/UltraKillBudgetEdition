using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : MonoBehaviour
{
    PlayerInventory playerInventory;
    HitIndicator hitIndicator;
    WeaponSwitching weaponSwitching;
    RaycastHit rayHit;
    ParticleSystem muzzleFlash;
    List<LineRenderer> energyTrails = new List<LineRenderer>();
    bool readyToShoot = true;

    [Header("Pistol Stats")]
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] float range;
    [SerializeField] float verticalSpread;
    [SerializeField] float horizontalSpread;
    [SerializeField] float effectsTime = 0.25f;
    [SerializeField] float minTrailDistance = 5f;
    [SerializeField] float initialTrailWidth = 0.5f;
    [SerializeField] int maxTrails = 10;

    [Header("References")]
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] GameObject lineRendererPrefab;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] BarrelRotator barrelRotator;
    Animator animatior;

    private void Awake()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        hitIndicator = FindFirstObjectByType<HitIndicator>();
        animatior = GetComponent<Animator>();
        weaponSwitching = GetComponentInParent<WeaponSwitching>();
    }

    private void OnEnable()
    {
        animatior.SetFloat("Speed", 1 / fireRate);
    }

    void Start()
    {
         InitializeTrails();
    }

    private void Update()
    {
        if (playerInventory.currentEnergyCellsCount > 0 && Input.GetKey(KeyCode.Mouse0) && !weaponSwitching.isSwitching)
        {
            barrelRotator.StartRotation();
            animatior.SetBool("Shooting", true);

            if (readyToShoot)
            {
                Shoot();
            }
        }
        else
        {
            barrelRotator.StopRotation();
            animatior.SetBool("Shooting", false);
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Vector3 trailEndPosition;

        // Generate Random Spread Angles
        float spreadAngleX = Random.Range(-horizontalSpread, horizontalSpread);
        float spreadAngleY = Random.Range(-verticalSpread, verticalSpread);
        float spreadAngleZ = Random.Range(-horizontalSpread, horizontalSpread);

        // Apply Spread Using Rotation
        Quaternion spreadRotation = Quaternion.Euler(spreadAngleY, spreadAngleX, spreadAngleZ);
        Vector3 direction = spreadRotation * playerCamera.transform.forward; // Rotating the original forward vector

        if (Physics.Raycast(playerCamera.transform.position, direction, out rayHit, range))
        {
            IDamagable damagable = rayHit.collider.GetComponent<IDamagable>();
            if (damagable != null && !rayHit.collider.CompareTag("Player"))
            {
                damagable.Damage(damage, rayHit.collider);
                hitIndicator.Hit();
                if (rayHit.collider.CompareTag("Enemy"))
                {
                    Instantiate(bloodPrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                }
            }
            else
            {
                // hit wall game object or particle effect
            }

            trailEndPosition = rayHit.point;
        }
        else
        {
            trailEndPosition = playerCamera.transform.position + (direction * range);
        }

        if (Vector3.Distance(muzzleFlash.transform.position, trailEndPosition) >= minTrailDistance)
        {
            DrawTrail(muzzleFlash.transform.position + (muzzleFlash.transform.forward * 0.1f), trailEndPosition);
        }

        muzzleFlash.Play();
        Invoke("ResetShot", fireRate);
        playerInventory.RemoveEnergyCells(1);
    }


    void DrawTrail(Vector3 start, Vector3 end)
    {
        LineRenderer energyTrail = GetAvailableLineRenderer();
        if (energyTrail != null)
        {
            energyTrail.SetPosition(0, start);
            energyTrail.SetPosition(1, end);
            energyTrail.widthMultiplier = initialTrailWidth;
            energyTrail.enabled = true;
            StartCoroutine(DisableTrail(energyTrail));
        }
    }

    LineRenderer GetAvailableLineRenderer()
    {
        foreach (LineRenderer trail in energyTrails)
        {
            if (!trail.enabled)
            {
                return trail;
            }
        }
        return null;
    }

    IEnumerator DisableTrail(LineRenderer energyTrail)
    {
        float elapsedTime = 0f;

        while (elapsedTime < effectsTime)
        {
            energyTrail.widthMultiplier = Mathf.Lerp(initialTrailWidth, 0, elapsedTime / effectsTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        energyTrail.widthMultiplier = 0;
        energyTrail.enabled = false;
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    private void InitializeTrails()
    {
        for (int i = 0; i < maxTrails; i++)
        {
            GameObject lineRendererObject = Instantiate(lineRendererPrefab, transform);
            LineRenderer lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            energyTrails.Add(lineRenderer);
        }
    }

    private void OnDisable()
    {
        ResetAllTrails();
    }

    private void ResetAllTrails()
    {
        foreach (LineRenderer trail in energyTrails)
        {
            trail.enabled = false;
        }
    }
}
