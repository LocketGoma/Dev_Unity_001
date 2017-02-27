using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public Transform enemyPrefab;

    public Transform spawnPoint;


    public float timeBetweenWaves = 5f; // 웨이브 시간 간격
    private float countdown = 2f;

    private int waveLevel = 1;

    public static bool breaksign = false;

    void Update()
    {
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }
    void SpawnWave()
    {
        //Debug.Log("Prepare for Wave");
       // for (int i = 0; i < waveLevel; i++)
       // {
            SpawnEnemy();
       // }
       // waveLevel++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);
        Debug.Log("enemy spawn \n");
    }

    public bool sign()
    {
        return breaksign;
    }
    public void changesign()
    {
        if (this.sign() == false)
        {
            breaksign = true;
        }
        else
            breaksign = false;
    }


}
