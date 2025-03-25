using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPadStrength = 10f;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpPadStrength , ForceMode.Impulse);
        }
    }
}
