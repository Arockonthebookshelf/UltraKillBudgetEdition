using UnityEngine;

public class Wave : MonoBehaviour
{
    WaveManager manager;
    [SerializeField]private Spawner[] spawners;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveManager>();
    }
    private void Update()
    {
        
    }
    public void WaveStart()
    {

    }
    public void WaveUpdate()
    {

    }
    public void WaveStop()
    {

    }
}
