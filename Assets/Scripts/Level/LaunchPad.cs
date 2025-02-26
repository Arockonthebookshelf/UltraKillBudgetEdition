using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.linearVelocity = Vector3.zero;
            Vector3 direction = (targetTransform.position - collision.transform.position).normalized;
            float distance = Vector3.Distance(collision.transform.position, targetTransform.position);
            float requiredForce = Mathf.Sqrt(distance * Physics.gravity.magnitude * 2); 
            playerRigidbody.AddForce(direction * requiredForce, ForceMode.VelocityChange);
        }
    }
}
