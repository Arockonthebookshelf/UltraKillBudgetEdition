using UnityEngine;



public class SingleSpawner : MonoBehaviour
{
    [SerializeField]GameObject spawnObject;
    private GameObject objs;
    public void Start()
    {
        if (spawnObject == null)
        {
            Debug.LogWarning("Object to be spawned cannot be empty");
            return;
        }
       
            GameObject obj = Instantiate(spawnObject, transform.position , Quaternion.identity, transform);
            objs = obj;
            obj.SetActive(false);
    }
    public void Spawn()
    {
        if (spawnObject == null)
        {
            return;
        }
        objs.SetActive(true);
    }
}
