using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingSpeed = 6f;
    public float bobbingAmount = 0.1f;
    public float midpoint = 0f; 

    private float timer = 0f;
    private PlayerMovement player;
    private Vector3 initialPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (player != null && player.grounded && !player.IsCrouching())
        {
            if (player.GetVelocity().magnitude > 0.5f)
            {
                timer += Time.deltaTime * bobbingSpeed;
                float bobOffset = Mathf.Sin(timer) * bobbingAmount;
                transform.localPosition = new Vector3(initialPosition.x, midpoint + bobOffset, initialPosition.z);
            }
            else
            {
                timer = 0;
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
            }
        }
    }
}
