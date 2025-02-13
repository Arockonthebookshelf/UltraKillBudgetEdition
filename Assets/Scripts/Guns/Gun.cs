using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] public int maxAmmo;
    [SerializeField] protected float fireRate;
    public int currentAmmo;
    protected abstract void Shoot();
    protected abstract void Reload();
}
