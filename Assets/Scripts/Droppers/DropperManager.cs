using UnityEngine;
using System.Collections;

public class DropperManager : MonoBehaviour
{
    [HideInInspector] public Collider playerCollider;
    void Awake()
    {
        playerCollider = PlayerMovement.Instance.GetComponent<Collider>();
    }
    
    [Header("Health Dropper Settings")]
    public int healthPerPickup = 5;
    public bool canDropHealth = true;
    public short healthMinDropAmount = 1;
    public short healthMaxDropAmount = 3;

    [Header("shotgunAmmo Dropper Settings")]
    public int shotgunAmmoPickupMultiplier = 1;
    public bool canDropShotgunAmmo = true;
    public short shotgunAmmoMinDropAmount = 1;
    public short shotgunAmmoMaxDropAmount = 2;

    [Header("EnergyCells Dropper Settings")]
    public int energyCellsPickupMultiplier = 1;
    public bool canDropEnergyCells = true;
    public short energyCellsMinDropAmount = 3;
    public short energyCellsMaxDropAmount = 5;

    [Header("Rockets Dropper Settings")]
    public int rocketsPickupMultiplier = 1;
    public bool canDropRockets = true;
    public short rocketsMinDropAmount = 1;
    public short rocketsMaxDropAmount = 2;

    void Start()
    {
        StartCoroutine(CheckChildrenCoroutine());
    }

    IEnumerator CheckChildrenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (transform.childCount == 0)
            {
                Destroy(gameObject);
                yield break;
            }
        }
    }
}
