using UnityEngine;

public class Pistol : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] float range;
    [SerializeField] float verticalSpread;
    [SerializeField] float horizontalSpread;
    [SerializeField] float effectsTime;

    bool readyToShoot = true;

    [SerializeField] Camera playerCamera;
    RaycastHit rayHit;
    [SerializeField] LayerMask whatIsEnemy;


    ParticleSystem muzzleFlash;
    LineRenderer bulletTrail;

    private void Awake()
    {
        bulletTrail = GetComponent<LineRenderer>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (readyToShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        bulletTrail.SetPosition(0, muzzleFlash.transform.position);
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
                
            }

            trailEndPosition = rayHit.point;
        }
        else
        {
            trailEndPosition = playerCamera.transform.position + (playerCamera.transform.forward * range);
        }

        DrawTrail(trailEndPosition);
        muzzleFlash.Play();

        Invoke("ResetShot", fireRate);
    }

    void DrawTrail(Vector3 end)
    {
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
