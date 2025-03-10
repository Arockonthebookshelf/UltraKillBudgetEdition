using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    PlayerInventory playerInventory;

    RaycastHit rayHit;
    ParticleSystem muzzleFlash;
    LineRenderer bulletTrail;
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

    [Header("References")]
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask whatIsEnemy;


    private void Awake()
    {
        bulletTrail = GetComponent<LineRenderer>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    void Start()
    {
        initialTrailWidth = bulletTrail.widthMultiplier;
    }

    private void Update()
    {
        if (readyToShoot && playerInventory.currentBulletCount > 0 && Input.GetKeyDown(KeyCode.Mouse0))
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

        playerInventory.RemoveBullets(1);
    }

    void DrawTrail(Vector3 start, Vector3 end)
    {
        bulletTrail.SetPosition(0, start);
        bulletTrail.SetPosition(1, end);
        bulletTrail.widthMultiplier = initialTrailWidth;
        bulletTrail.enabled = true;
        StartCoroutine(DisableTrail());
    }

    IEnumerator DisableTrail()
    {
        float elapsedTime = 0f;

        while (elapsedTime < effectsTime)
        {
            bulletTrail.widthMultiplier = Mathf.Lerp(initialTrailWidth, 0, elapsedTime / effectsTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bulletTrail.widthMultiplier = 0;
        bulletTrail.enabled = false;
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

}
