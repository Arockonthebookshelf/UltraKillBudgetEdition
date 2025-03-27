using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSwitching weaponSwitching;
    [SerializeField] string bulletTag = "RocketLauncher Projectiles";
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;
    Animator animatior;

    private void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        animatior = GetComponent<Animator>();
        weaponSwitching = GetComponentInParent<WeaponSwitching>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void OnEnable()
    {
        animatior.SetFloat("Speed", 1 / fireRate);
    }

    private void HandleInput()
    {
        if (readyToShoot && playerInventory.currentRocketsCount > 0 && Input.GetKeyDown(KeyCode.Mouse0) && !weaponSwitching.isSwitching)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        animatior.SetTrigger("Shoot");
        // Calculate direction
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity))
        {
            // If we hit something, set the target point to that position
            targetPoint = hit.point;
        }
        else
        {
            // If we don't hit anything, shoot in the forward direction
            targetPoint = fpsCam.transform.position + fpsCam.transform.forward * 100f;
        }

        // Calculate direction from attack point to target point
        Vector3 direction = (targetPoint - attackPoint.position).normalized;

        // Instantiate Rocket and apply force
        GameObject rocket = ObjectPooler.Instance.SpawnProjectileFromPool(bulletTag, attackPoint.position, Quaternion.identity); ;
        Rigidbody rocketRB = rocket.GetComponent<Rigidbody>();
        rocket.SetActive(true);
        rocketRB.AddForce(direction * shootForce, ForceMode.Impulse);

        // Update player inventory
        playerInventory.RemoveRockets(1);
        playerInventory.CanShoot(false);

        // Reset shot after fire rate delay
        Invoke("ResetShot", fireRate);
    }


    private void ResetShot()
    {
        readyToShoot = true;
        playerInventory.CanShoot(true);
    }

}