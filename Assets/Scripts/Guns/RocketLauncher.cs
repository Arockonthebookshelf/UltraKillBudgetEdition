using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RocketLauncher : MonoBehaviour
{
    PlayerInventory playerInventory;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;

    private void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (readyToShoot && playerInventory.currentRocketsCount>0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        GameObject rocket = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        Rigidbody rocketRB = rocket.GetComponent<Rigidbody>();
        rocketRB.AddForce(fpsCam.transform.forward * shootForce, ForceMode.Impulse);
        playerInventory.RemoveRockets(1);
        Invoke("ResetShot", fireRate);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

}