using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float fireRate;
    protected int currentAmmo;
    protected abstract void Shoot();
    protected abstract void Reload();
}
