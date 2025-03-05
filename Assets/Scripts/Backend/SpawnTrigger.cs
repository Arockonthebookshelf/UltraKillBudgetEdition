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
        //spawnerPosition = GameObject.FindGameObjectWithTag("Spawner"); 
        RaycastHit hit;
        if(Physics.SphereCast(transform.position,spawnerDistance,new Vector3(1,0,1),out hit))
        {
            Debug.Log("cast");
            if(hit.collider.CompareTag("Spawner") && hit.collider.gameObject.GetComponent<Spawner>())
            {
                spawner =hit.collider.gameObject.GetComponent<Spawner>();
            }
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
    void OnTriggerEnter(Collider other)
    {
        
    }
}
