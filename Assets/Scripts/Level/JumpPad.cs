using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPadStrength = 10f;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * jumpPadStrength , ForceMode.Impulse);
        }
    }
}
