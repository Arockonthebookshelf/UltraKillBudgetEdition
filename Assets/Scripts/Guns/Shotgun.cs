using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Shotgun : MonoBehaviour
{
    PlayerInventory playerInventory;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;
    [SerializeField] float verticleSpread;
    [SerializeField] float horizontalSpread;
    [SerializeField] int bulletsPerTap;
    [SerializeField] int poolSize = 10;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;

    private Queue<GameObject> bulletPool;

    private void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        InitializeBulletPool();
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

        for (int i = 0; i < bulletsPerTap; i++)
        {
            Vector3 directionWithSpread = CalculateSpread();

            GameObject currentBullet = GetBulletFromPool();
            currentBullet.transform.position = attackPoint.position;
            currentBullet.transform.forward = directionWithSpread.normalized;
            currentBullet.SetActive(true);

            Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
            bulletRb.linearVelocity = Vector3.zero; // Reset velocity
            bulletRb.AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            bulletRb.AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }
        playerInventory.RemoveCapacitors(1);
        playerInventory.CanShoot(false);
        Invoke("ResetShot", fireRate);
    }

    private Vector3 CalculateSpread()
    {
        float spreadAngleX = Random.Range(-horizontalSpread, horizontalSpread);
        float spreadAngleY = Random.Range(-verticleSpread, verticleSpread);
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

    private void InitializeBulletPool()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            return bulletPool.Dequeue();
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            return bullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}