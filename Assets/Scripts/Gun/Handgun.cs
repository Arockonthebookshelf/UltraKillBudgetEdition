using UnityEngine;
using UnityEngine.InputSystem;

public class Handgun :Gun
{
    float lastFired;
    public Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        currentAmmo = maxAmmo;
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
        if(Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }
    protected override void Shoot()
    {
        Ray ray = new Ray { origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)) , direction = mainCamera.transform.forward };
        //RaycastHit []hit = new RaycastHit[5];
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(20f);
                --currentAmmo;
                Debug.Log(currentAmmo);
            }
        }
    }
    protected override void Reload()
    {
        currentAmmo = maxAmmo;
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(mainCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0)), mainCamera.transform.forward*5);
    }
    
}
