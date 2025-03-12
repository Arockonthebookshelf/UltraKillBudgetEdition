using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float fireRate;
    [SerializeField] float spread;
    [SerializeField] int bulletsPerTap;

    bool readyToShoot = true;
    public Camera fpsCam;
    public Transform attackPoint;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (readyToShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Invoke("ResetShot", fireRate);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

}