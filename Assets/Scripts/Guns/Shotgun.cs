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
    [SerializeField] float spread;
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

        Invoke("ResetShot", fireRate);
    }

    private Vector3 CalculateSpread()
    {
        Vector3 directionWithoutSpread = fpsCam.transform.forward;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);
        return directionWithoutSpread + new Vector3(x, y, z);
    }

    private void ResetShot()
    {
        readyToShoot = true;
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