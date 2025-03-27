using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth = 5f;
    [SerializeField] private float multiplier = 1f;

    [Header("Bob Settings")]
    [SerializeField] private float bobSpeed = 5f;
    [SerializeField] private float bobAmount = 0.05f;

    private Vector3 initialPosition;
    private float timer;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        ApplySway();
        ApplyBob();
    }

    private void ApplySway()
    {
        float mouseX = PlayerMovement.Instance.mouseInput.x * multiplier;
        float mouseY = PlayerMovement.Instance.mouseInput.y * multiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    private void ApplyBob()
    {
        if (PlayerMovement.Instance.GetVelocity().magnitude > 0.5f && PlayerMovement.Instance.grounded && !PlayerMovement.Instance.IsCrouching())
        {
            timer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(timer) * bobAmount;
            transform.localPosition = initialPosition + new Vector3(0, bobOffset, 0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * bobSpeed);
        }
    }
}