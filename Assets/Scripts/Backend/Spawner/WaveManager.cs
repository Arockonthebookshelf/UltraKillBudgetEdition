using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Tooltip("Gap between waves")]
    public int waveCountDown;
    private Wave wave = new Wave();
    private List<Spawner> _spawners = new List<Spawner>();
    private int currentWave;
    private int waveMax;
    private int _waveGrowth;
    private bool waveIsActive;

    private void OnEnable()
    {
        WaveTrigger.OnSpawnerTriggered += WaveStart;
        wave.OnCurrentEventStop += WavesUpdate;
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(waveIsActive)
        {
            wave.WaveUpdate();
        }
    }

    void WaveStart(List<Spawner> spawners,int waveLimit,int waveGrowth)
    {
        _spawners = spawners;
        waveMax = waveLimit;
        _waveGrowth = waveGrowth;
        currentWave = 0;
        StartCoroutine(WaitTime());
        wave.WaveSpawn(spawners);
        waveIsActive = true;
    }
    void WavesUpdate()
    {
        if(currentWave < waveMax)
        {
            currentWave++;
            foreach(Spawner spawner in _spawners)
            {
                spawner.IncreaseBatchSize(_waveGrowth);
            };
            wave.WaveSpawn(_spawners);
            waveIsActive = true;
        }
        else
        {
            foreach(Spawner spawner in _spawners)
            {
                spawner.gameObject.SetActive(false);
            }
            waveIsActive = false;
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        WaveTrigger.OnSpawnerTriggered -= WaveStart;
        wave.OnCurrentEventStop -= WavesUpdate;
    }
    IEnumerator WaitTime()
    {
        while(true)
        {
            Debug.Log("before wait");
            yield return new WaitForSeconds(waveCountDown);
            Debug.Log("after wait");
        }
        
    }
}
