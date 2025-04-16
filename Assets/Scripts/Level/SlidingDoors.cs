using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
[SerializeField] List<Material> unlockedMaterial;
[SerializeField] List<Material> lockedMaterial;
[SerializeField] MeshRenderer indicator;
Animator slidingDoorsAnimator;
[SerializeField] BoxCollider doorCollider;
bool doorsOpen;
public bool locked;

    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;
    void Awake()
{
    slidingDoorsAnimator = GetComponent<Animator>();
}

void Start()
{
       if(locked)
       {
            indicator.SetMaterials(lockedMaterial);
       }
       else
       {
            indicator.SetMaterials(unlockedMaterial);
       }
}

void OnTriggerEnter(Collider other)
{
        
    if(other.CompareTag("Player") && !locked)
    {
        doorOpenSound.Play();
        slidingDoorsAnimator.Play("DoorOpen");
        doorsOpen = true;
        doorCollider.enabled = false;
    }
}

void OnTriggerExit(Collider other)
{
        
        if (other.CompareTag("Player") && doorsOpen)
    {
        doorCloseSound.Play();
        slidingDoorsAnimator.Play("DoorClose");
        doorsOpen = false;
        doorCollider.enabled = true;
    }
}

public void UnlockDoor()
{
    locked = false;
    indicator.SetMaterials(unlockedMaterial);
}
public void LockDoor()
{
    locked = true;
    indicator.SetMaterials(lockedMaterial);
}
}
