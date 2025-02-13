using UnityEngine;

public class TestSubject : MonoBehaviour , IDamagable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage, Collider hitCollider)
    {
        Debug.Log(damage);
    }
}
