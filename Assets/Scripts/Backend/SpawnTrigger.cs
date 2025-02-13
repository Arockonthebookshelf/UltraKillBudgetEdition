using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]GameObject SpawnObject;
    GameObject spawnerPosition;
    private Spawner spawner;
    [SerializeField][Range(0,10)]private int spawnerDistance;
    void Start()
    {
        spawnerPosition = GameObject.FindGameObjectWithTag("Spawner"); 
        RaycastHit hit;
        if(Physics.SphereCast(transform.position,spawnerDistance,Vector3.zero,out hit))
        {
            hit.collider.CompareTag("Spawner");
        }
        else
        {
            Debug.LogWarning("Spawner is not in radius, increase distance or add the spawner near the trigger");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
