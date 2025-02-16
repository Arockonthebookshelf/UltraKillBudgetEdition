using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Spawner[] spawners { get { return _spawners; } }
    private Spawner[] _spawners;
    void Start()
    {
        
    }
    public void GetSpawner(Spawner[] _spawners)
    {
        this._spawners = _spawners;
    }
    void Update()
    {
        
    }
    
    
}

