using UnityEngine;

public class Disable : MonoBehaviour
{
    public int diableTimer;
    // Update is called once per frame
    void OnEnable()
    {
        Invoke(nameof(DisableObj), diableTimer);
    }
    void DisableObj()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        ObjectPooler.Instance.EnqueObject(transform.parent.name, gameObject);
        gameObject.SetActive(false);
    }
}
