using UnityEngine;

public class Shotgun : MonoBehaviour
{
    PlayerInventory playerInventory;
    [SerializeField] string bulletTag = "Shotgun Projectile";
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;
    [SerializeField] float verticalSpread;
    [SerializeField] float horizontalSpread;
    [SerializeField] int bulletsPerTap;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;
    Animator animator;

    private void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Speed", 1 / fireRate);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (readyToShoot && playerInventory.currentCapacitorCount > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        animator.SetTrigger("Shot");
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

        playerInventory.RemoveCapacitors(1);
        playerInventory.CanShoot(false);
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
        playerInventory.CanShoot(true);
    }
}
