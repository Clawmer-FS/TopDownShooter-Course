using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    public GameObject boss;
    public Transform bossSpwanPoint;

    private bool finishedSpawning;
    public Text textWave;

    public GameObject healthBar;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
        textWave.text = "Wave: " + (currentWaveIndex+1) + " / " + waves.Length;
    }

    private void Update()
    {
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length) 
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
                textWave.text = "Wave: " + (currentWaveIndex+1) + " / " + waves.Length;
            }    
            else
            {
                Instantiate(boss, bossSpwanPoint.position, bossSpwanPoint.rotation);
                healthBar.SetActive(true);
                textWave.text = "Wave: Boss";
            }
        }
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave (int index)
    {
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if(i == currentWave.count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }


}
