using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera cam;
    [SerializeField] GameObject crossHair;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        crossHair.transform.position = cam.WorldToViewportPoint(cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)));
    }
}
