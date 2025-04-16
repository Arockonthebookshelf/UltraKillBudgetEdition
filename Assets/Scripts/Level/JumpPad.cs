using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPadStrength = 10f;
    public AudioSource jumpPadSound;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            jumpPadSound.Play();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpPadStrength , ForceMode.Impulse);
        }
    }
}
