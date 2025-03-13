using UnityEngine;

public class PathWay : MonoBehaviour
{
    [SerializeField]private Transform pointA;
    [SerializeField]private Transform pointB;
    private Transform currentTarget;    
    void Start()
    {
        gameObject.tag = "PathWay";
        currentTarget = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform currentTargetPoint()
    {
        return currentTarget;
    }
    public void ChangeTarget()
    {
        if(currentTarget == pointA)
        {
            currentTarget = pointB;
        }
        else
        {
            currentTarget = pointA;
        }
    }
}
