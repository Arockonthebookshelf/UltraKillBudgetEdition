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

    [Header("References")]
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask whatIsEnemy;

    private void Awake()
    {
        bulletTrail = GetComponent<LineRenderer>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
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

        //Spread
        float x = Random.Range(-horizontalSpread, horizontalSpread);
        float y = Random.Range(-verticalSpread, verticalSpread);

        //Calculate Direction with Spread
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
                //bullet hole game object or particle effect
            }

            trailEndPosition = rayHit.point;
        }
        else
        {
            trailEndPosition = playerCamera.transform.position + (playerCamera.transform.forward * range);
        }

        
        if(Vector3.Distance(muzzleFlash.transform.position, rayHit.point) >= minTrailDistance)
        {
            DrawTrail(muzzleFlash.transform.position ,trailEndPosition);
        }
    
        muzzleFlash.Play();

        Invoke("ResetShot", fireRate);

        playerInventory.RemoveBullets(1);
    }

    void DrawTrail(Vector3 start, Vector3 end)
    {
        bulletTrail.SetPosition(0, start);
        bulletTrail.SetPosition(1, end);
        bulletTrail.enabled = true;
        Invoke("DisableTrail", effectsTime);
    }

    void DisableTrail()
    {
        bulletTrail.enabled = false;
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

}
