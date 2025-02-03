using TMPro;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text ammoCount;
    int mAmmo;

    public void UpdateWeapon(string name , int currentAmmo , int maxAmmo)
    {
        weaponName.SetText(name + ":");
        ammoCount.SetText(currentAmmo + "/" + maxAmmo);
        mAmmo = maxAmmo;
    }

    public void UpdateAmmo(int currentAmmo)
    {
        ammoCount.SetText(currentAmmo + "/" + mAmmo);
    }

    void Start()
    {
        UpdateWeapon("Handgun",6,10);
    }
}
