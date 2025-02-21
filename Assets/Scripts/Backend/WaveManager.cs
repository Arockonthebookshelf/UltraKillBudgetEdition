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
            Debug.Log("wave"+ currentWave);
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
        Debug.Log("waves change");
        if(currentWave < waveMax)
        {
            currentWave++;
            foreach(Spawner spawner in _spawners)
            {
                spawner.IncreaseBatchSize(_waveGrowth);
            }

            WaitTime();
            wave.WaveSpawn(_spawners);
            waveIsActive = true;
        }
        else
        {
            Debug.Log("Wave over");
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
        Debug.Log("Cooroutine works");
        while(true)
        {
            yield return new WaitForSeconds(waveCountDown);
        }
        
    }
}
