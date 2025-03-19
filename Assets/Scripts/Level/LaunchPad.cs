using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float flightTime = 1.0f;
    bool canLaunch = true;

    void OnTriggerEnter(Collider other)
    {
        if (!canLaunch || !other.gameObject.CompareTag("Player")) return;

        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (playerRigidbody == null) return;

        // Reset existing velocity to ensure precision
        Vector3 currentVelocity = playerRigidbody.linearVelocity;
        playerRigidbody.linearVelocity = Vector3.zero;

        Vector3 displacement = targetTransform.position - other.transform.position;
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0, displacement.z);

        // Calculate required velocity considering gravity and current velocity
        float verticalVelocity = (displacement.y / flightTime) + (0.5f * Physics.gravity.magnitude * flightTime);
        float horizontalVelocity = horizontalDisplacement.magnitude / flightTime;

        Vector3 launchVelocity = (horizontalDisplacement.normalized * horizontalVelocity) + (Vector3.up * verticalVelocity);

        // Apply force, considering the player's current velocity
        Vector3 requiredForce = (launchVelocity - currentVelocity) * playerRigidbody.mass;
        playerRigidbody.AddForce(requiredForce, ForceMode.Impulse);

        canLaunch = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canLaunch = true;
    }
}
