using PrometheanUprising.SoundManager;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    
    WeaponSwitching weaponSwitching;
    [SerializeField] string bulletTag = "Shotgun Projectile";
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;
    [SerializeField] float verticalSpread;
    [SerializeField] float horizontalSpread;
    [SerializeField] int bulletsPerTap;
    [SerializeField] ParticleSystem muzzleFlash;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;
    Animator animatior;

    private void Awake()
    {
        
        animatior = GetComponent<Animator>();
        weaponSwitching = GetComponentInParent<WeaponSwitching>();
    }

    private void OnEnable()
    {
        animatior.SetFloat("Speed", 1 / fireRate);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (readyToShoot && PlayerInventory.instance.currentshotgunAmmoCount > 0 && Input.GetKeyDown(KeyCode.Mouse0) && !weaponSwitching.isSwitching && PlayerMovement.Instance.inputEnabled)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        SoundManager.PlaySound(SoundType.SHOTGUN_FIRE);

        animatior.SetTrigger("Shoot");
        muzzleFlash.Play();

        for (int i = 0; i < bulletsPerTap; i++)
        {
            Vector3 directionWithSpread = CalculateSpread();

            GameObject currentBullet = ObjectPooler.Instance.SpawnProjectileFromPool(bulletTag, attackPoint.position, Quaternion.identity);
            if (currentBullet != null)
            {
                currentBullet.transform.forward = directionWithSpread.normalized;
                currentBullet.SetActive(true);

                Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
                bulletRb.linearVelocity = Vector3.zero;
                bulletRb.AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
                bulletRb.AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            }
        }

        PlayerInventory.instance.RemoveShotgunAmmo(1);
        PlayerInventory.instance.CanShoot(false);
        Invoke("ResetShot", fireRate);
    }


    private Vector3 CalculateSpread()
    {
        float spreadAngleX = Random.Range(-horizontalSpread, horizontalSpread);
        float spreadAngleY = Random.Range(-verticalSpread, verticalSpread);
        float spreadAngleZ = Random.Range(-horizontalSpread, horizontalSpread);

        // Apply Spread Using Rotation
        Quaternion spreadRotation = Quaternion.Euler(spreadAngleY, spreadAngleX, spreadAngleZ);
        Vector3 direction = spreadRotation * fpsCam.transform.forward;
        return direction;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        PlayerInventory.instance.CanShoot(true);
    }
}
