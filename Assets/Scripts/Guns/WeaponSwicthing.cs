using UnityEngine;

public class WeaponSwicthing : MonoBehaviour
{
    HUD hud;
    PlayerInventory playerInventory;
    [Header("References")]
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject minigun;
    [SerializeField] GameObject rocketLauncher;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    GameObject selectedWeapon;
    GameObject previousSelectedWeapon;
    float timeSinceLastSwicth;

    void Awake()
    {
        hud = FindFirstObjectByType<HUD>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    void Start()
    {
        previousSelectedWeapon = pistol;
        timeSinceLastSwicth = 0f;
    }

    private void Update()
    {
        if(timeSinceLastSwicth > switchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerInventory.hasPistol)
            {
                selectedWeapon = pistol;
                CheckWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && playerInventory.hasShotgun)
            {
                selectedWeapon = shotgun;
                CheckWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && playerInventory.hasMinigun)
            {
                selectedWeapon = minigun;
                CheckWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && playerInventory.hasRocketLauncher)
            {
                selectedWeapon = rocketLauncher;
                CheckWeapon();
            }
        }

        timeSinceLastSwicth += Time.deltaTime;
    }

    void CheckWeapon()
    {
        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }
    }
    private void Select(GameObject weapon)
    {
        selectedWeapon.SetActive(true);
        previousSelectedWeapon.SetActive(false);
        previousSelectedWeapon = selectedWeapon;
        timeSinceLastSwicth = 0f;
        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        hud.UpdateWeapon(selectedWeapon.name);
    }
}
