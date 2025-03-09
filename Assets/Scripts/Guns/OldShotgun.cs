using System.Collections.Generic;
using UnityEngine;

public class OldShotgun : Gun
{
    [SerializeField]private Camera mainCamera;
    [SerializeField]private Transform fireArea;
    [SerializeField]private GameObject bullet;
    [SerializeField]private Transform container;
    private int bulletLimit = 12;
    [SerializeField] private List <GameObject> bullets;
    private float lastFired;

    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < bulletLimit; i++)
        {
            bullets.Add(Instantiate(bullet));
            bullets[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastFired = lastFired + Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && lastFired > fireRate && currentAmmo > 0)
        {
            lastFired = 0;
            Shoot();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }
    protected override void Shoot()
    {
        Ray ray = new Ray { origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), direction = mainCamera.transform.forward };
        //RaycastHit []hit = new RaycastHit[5];
        Ray fireRay = new Ray { origin = fireArea.position , direction = mainCamera.transform.forward };
        RaycastHit hit;
        if (Physics.Raycast(fireRay, out hit))
        {
            Vector3 pos = hit.point;
            Debug.Log(pos);
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
            --currentAmmo;
            if (damagable != null)
            {
               // damagable.Damage(20f);
            }
        }
        
    }
    protected override void Reload()
    {
        currentAmmo = maxAmmo;
    }
    private void OnDrawGizmos()
    {
        Vector3 _fireareacords = fireArea.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), mainCamera.transform.forward * 5);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_fireareacords, mainCamera.transform.forward*5);
        Gizmos.color = Color.blue;
    }
}

