using UnityEngine;

public class UIBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingSpeed = 4f;
    public float bobbingAmount = 5f;

    [Header("Look Sway Settings")]
    public float lookSwayAmount = 10f;

    [Header("Hit Effect Settings")]
    public float hitEffectAmount = 10f;
    public float hitEffectDuration = 0.2f;

    private Vector2 initialPosition;
    private float timer = 0f;
    private Transform playerCamera;
    private RectTransform rectTransform;
    private float hitEffectTimer = 0f;

    void Start()
    {
        playerCamera = Camera.main.transform;
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (rectTransform != null && PlayerMovement.Instance.grounded && !PlayerMovement.Instance.IsCrouching())
        {
            Vector2 newPosition = initialPosition;

            if (PlayerMovement.Instance.GetVelocity().magnitude > 0.5f)
            {
                timer += Time.deltaTime * bobbingSpeed;
                float bobOffset = Mathf.Sin(timer) * bobbingAmount;
                newPosition.y += bobOffset;
            }
            else
            {
                timer = 0;
            }

            float lookSway = -playerCamera.transform.localRotation.y * lookSwayAmount;
            newPosition.x += lookSway;

            if (hitEffectTimer > 0)
            {
                newPosition.y += hitEffectAmount;
                hitEffectTimer -= Time.deltaTime;
            }

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, Time.deltaTime * 5f);
        }
    }

    public void TriggerHitEffect()
    {
        hitEffectTimer = hitEffectDuration;
    }
}
