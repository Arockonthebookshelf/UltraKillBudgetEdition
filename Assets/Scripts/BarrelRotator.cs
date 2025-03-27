using UnityEngine;

public class BarrelRotator : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public float acceleration = 2f;  
    public float deceleration = 2f;  
    private float currentSpeed = 0f; 
    private bool isRotating = false;   

    void Update()
    {

        if (isRotating)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, rotationSpeed, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * deceleration);
        }

        transform.localRotation *= Quaternion.Euler(0, 0, currentSpeed * Time.deltaTime);
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }
}
