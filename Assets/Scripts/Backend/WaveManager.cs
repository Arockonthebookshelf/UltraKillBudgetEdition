using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Tooltip("Gap between waves")]
    public int waveCountDown;
    private Wave wave = new Wave();
    private List<Spawner> _spawners = new List<Spawner>();
    public int currentWave;
    private int waveMax;
    bool waitForWave;
    private int _waveGrowth;
    private bool waveIsActive;

    public static Action waveStart;

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
        if(waveIsActive )
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
        wave.WaveSpawn(spawners);
        waveIsActive = true;
    }
    void WavesUpdate()
    {
        if(currentWave < waveMax)
        {
            foreach(Spawner spawner in _spawners)
            {
                spawner.IncreaseBatchSize(_waveGrowth);
            }
            currentWave++;
            waitForWave = true;
            StartCoroutine("SpawnAfterWaitTime");
            // wave.WaveSpawn(_spawners);
            // waveIsActive = true;
        }
        else
        {
            Debug.Log("Wave over");
            foreach(Spawner spawner in _spawners)
            {
                Destroy(spawner.gameObject);
                //spawner.gameObject.SetActive(false);
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
    IEnumerator SpawnAfterWaitTime()
    {
        while(waitForWave)
        {
            yield return new WaitForSeconds(waveCountDown);
            wave.WaveSpawn(_spawners);
            waveIsActive = true;
            waitForWave = false;
        }
    }
}
