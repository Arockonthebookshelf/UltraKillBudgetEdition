using UnityEngine;

public class SpawnerPosition : MonoBehaviour
{
    
    private Transform position;
    public Transform Transform{get{return position;}}
    //public bool[] isActive=new bool[10];
    void Start()
    {
       position = transform;
    }

}
