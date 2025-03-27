using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour
{
    HUD hud;
    PlayerInventory playerInventory;

    [Header("References")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject minigun;
    [SerializeField] private GameObject rocketLauncher;

    [Header("Settings")]
    [SerializeField] private float switchTime = 0.5f;

    private GameObject selectedWeapon;
    private GameObject previousSelectedWeapon;
    public bool isSwitching = false;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    void Start()
    {
        previousSelectedWeapon = pistol;
    }

    private void Update()
    {
        if (isSwitching) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && playerInventory.hasPistol)
            StartCoroutine(SwitchWeaponWithAnimation(pistol));

        if (Input.GetKeyDown(KeyCode.Alpha2) && playerInventory.hasShotgun)
            StartCoroutine(SwitchWeaponWithAnimation(shotgun));

        if (Input.GetKeyDown(KeyCode.Alpha3) && playerInventory.hasMinigun)
            StartCoroutine(SwitchWeaponWithAnimation(minigun));

        if (Input.GetKeyDown(KeyCode.Alpha4) && playerInventory.hasRocketLauncher)
            StartCoroutine(SwitchWeaponWithAnimation(rocketLauncher));
    }

    private IEnumerator SwitchWeaponWithAnimation(GameObject newWeapon)
    {
        if (previousSelectedWeapon == newWeapon) yield break;

        isSwitching = true;
        float elapsedTime = 0f;
        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(90, 0, 0);

        while (elapsedTime < switchTime / 2)
        {
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(startRot, targetRot, elapsedTime / (switchTime / 2));
            yield return null;
        }

        previousSelectedWeapon.SetActive(false);
        newWeapon.SetActive(true);
        previousSelectedWeapon = newWeapon;
        OnWeaponSelected();

        elapsedTime = 0f;
        while (elapsedTime < switchTime / 2)
        {
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRot, startRot, elapsedTime / (switchTime / 2));
            yield return null;
        }

        isSwitching = false;
    }

    private void OnWeaponSelected()
    {
        hud.UpdateWeapon(previousSelectedWeapon.name);
    }
}
