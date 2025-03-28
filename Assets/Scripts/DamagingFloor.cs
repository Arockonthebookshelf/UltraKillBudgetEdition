using System.Collections;
using UnityEngine;

public class DamagingFloor : MonoBehaviour
{
    bool isPlayerOnFloor = false;
    [SerializeField] float damageAmount;
    [SerializeField] float damageInterval = 1f;
    
    private Coroutine damageCoroutine;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamagable player = collision.gameObject.GetComponent<IDamagable>();
            if (player != null)
            {
                isPlayerOnFloor = true;
                damageCoroutine = StartCoroutine(DamagePlayer(player));
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPlayerOnFloor)
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                isPlayerOnFloor = false;
            }
        }
    }

    IEnumerator DamagePlayer(IDamagable player)
    {
        while (isPlayerOnFloor)
        {
            player.Damage(damageAmount, null);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
