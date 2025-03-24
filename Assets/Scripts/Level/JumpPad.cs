using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPadStrength = 10f;
    [SerializeField] float cooldown = 1f;
    bool canJump = true;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && canJump)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpPadStrength , ForceMode.Impulse);
            canJump = false;
            Invoke("Reset", cooldown);
        }
    }
    private void Reset()
    {
        canJump = true;
    }
}
