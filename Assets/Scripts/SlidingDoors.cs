using Unity.VisualScripting;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
Animator slidingDoorsAnimator;
bool doorsOpen;
public bool locked;

void Awake()
{
    slidingDoorsAnimator = GetComponent<Animator>();
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

}
