using UnityEngine;

public class WeaponSwicthing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float swicthTime;

    private int selectedWeapon;
    private float timeSinceLastSwicth;

    void Start()
    {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwicth = 0f;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) keys = new KeyCode[weapons.Length];
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwicth >= swicthTime)
            {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }

        timeSinceLastSwicth += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        timeSinceLastSwicth = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        Debug.Log("Selected New Weapon");
    }
}
