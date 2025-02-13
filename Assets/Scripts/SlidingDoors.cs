using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
[SerializeField] List<Material> unlockedMaterial;
[SerializeField] List<Material> lockedMaterial;
MeshRenderer meshRenderer;
Animator slidingDoorsAnimator;
bool doorsOpen;
public bool locked;

void Awake()
{
    slidingDoorsAnimator = GetComponent<Animator>();
    meshRenderer = GetComponentInChildren<MeshRenderer>();
}

void Start()
{
       if(locked)
       {
            meshRenderer.SetMaterials(lockedMaterial);
       }
       else
       {
            meshRenderer.SetMaterials(unlockedMaterial);
       }
}

void OnTriggerEnter(Collider other)
{
    if(other.CompareTag("Player") && !locked)
    {
        slidingDoorsAnimator.Play("DoorOpen");
        doorsOpen = true;
    }
}

void OnTriggerExit(Collider other)
{
    if(other.CompareTag("Player") && doorsOpen)
    {
        slidingDoorsAnimator.Play("DoorClose");
        doorsOpen = false;
    }
}

public void UnlockDoor()
{
    locked = false;
    meshRenderer.SetMaterials(unlockedMaterial);
}
public void LockDoor()
{
    locked = true;
    meshRenderer.SetMaterials(lockedMaterial);
}
}
