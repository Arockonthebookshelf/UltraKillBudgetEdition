using UnityEngine;
using System.Collections;

public class DropperManager : MonoBehaviour
{
    public Collider playerCollider;

    [Header("Bullets Dropper Settings")]
    public bool canDropBullets = true;
    public short bulletsMinDropAmount = 1;
    public short bulletsMaxDropAmount = 5;

    [Header("Capacitor Dropper Settings")]
    public bool canDropCapacitors = true;
    public short capacitorsMinDropAmount = 1;
    public short capacitorsMaxDropAmount = 2;

    [Header("EnergyCells Dropper Settings")]
    public bool canDropEnergyCells = true;
    public short energyCellsMinDropAmount = 3;
    public short energyCellsMaxDropAmount = 5;

    [Header("Rockets Dropper Settings")]
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
