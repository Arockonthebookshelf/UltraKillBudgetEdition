using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : MonoBehaviour
{
    PlayerInventory playerInventory;

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

    private void Awake()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        InitializeTrails();
    }

    void Start()
    {
        initialTrailWidth = energyTrails[0].widthMultiplier;
    }

    private void Update()
    {
        if (readyToShoot && playerInventory.currentEnergyCellsCount > 0 && Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Vector3 trailEndPosition;

        // Spread
        float x = Random.Range(-horizontalSpread, horizontalSpread);
        float y = Random.Range(-verticalSpread, verticalSpread);

        // Calculate Direction with Spread
        Vector3 direction = playerCamera.transform.forward + new Vector3(x, y, 0);
        
        if (Physics.Raycast(playerCamera.transform.position, direction, out rayHit, range))
        {
            IDamagable damagable = rayHit.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(damage, rayHit.collider);
            }
            else
            {
                // bullet hole game object or particle effect
            }

            trailEndPosition = rayHit.point;
        }
        else
        {
            trailEndPosition = playerCamera.transform.position + (playerCamera.transform.forward * range);
        }

        if(Vector3.Distance(muzzleFlash.transform.position, rayHit.point) >= minTrailDistance)
        {
            DrawTrail(muzzleFlash.transform.position + (muzzleFlash.transform.forward * 0.5f), trailEndPosition);
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
}
