using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWave : MonoBehaviour
{
    [SerializeField] GameObject nextWave;
    [SerializeField] bool isLastWave;
    [SerializeField] SlidingDoors doorToUnlock;
    [SerializeField] List<GameObject> enemiess;
    int enemies;

    void Start()
    {
        enemies = transform.childCount;
        StartCoroutine(CheckChildrenCoroutine());
    }

    public void SpawnEnemies()
    {
        for( int i = 0; i < enemies ; i++)
        {

            transform.GetChild(i).gameObject.SetActive(true);
            enemiess.Add(transform.GetChild(i).gameObject);
    }
    }
    IEnumerator CheckChildrenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (transform.childCount == 0)
            {
                if(!isLastWave)
                {
                    nextWave.GetComponent<SingleWave>().SpawnEnemies();
                }
                else
                {
                    if(doorToUnlock != null)
                    {
                        doorToUnlock.UnlockDoor();
                    }
                }

                Destroy(gameObject);
            }
        }
    }
    }

