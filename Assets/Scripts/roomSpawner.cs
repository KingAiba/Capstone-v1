using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int numOfEnemies;
    public List<GameObject> enemyPrefabs;
    public float spawnDelay;
    public bool isSpawningComplete = false;

    public bool isBossWave = false;
    public GameObject BossPrefabs;
}
public class roomSpawner : MonoBehaviour
{
    public List<Wave> waves;
    public List<Transform> spawnPoints;

    public List<Transform> bossSpawnPoint;
    public List<Transform> objectivePoints;

    public Wave currentWave;
    public int currWaveIndex = -1;

    public bool canSpawnWave = true;
    public bool bossSpawned = false;

    public bool isActive = false;

    public List<GameObject> currentEnemies = new List<GameObject>();
    public UnityEngine.AI.NavMeshSurface roomNavMeshSurface;

    public delegate void OnRoomCompleteDelegate();
    public OnRoomCompleteDelegate OnRoomComplete;

    public Transform GenerateRandomSpawnPoint(List<Transform> sp)
    {
        return sp[Random.Range(0, spawnPoints.Count)];
    }

    public GameObject GenerateRandomEnemy()
    {
        return currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Count)];
    }

    public void SpawnWave()
    {
        if(canSpawnWave)
        {
            StartCoroutine(SpawningCorutine());
        }
        else if(currentEnemies.Count <= 0 && currentWave.isSpawningComplete) 
        {
            if (currentWave.isBossWave && !bossSpawned)
            {
                SpawnBoss();
                
            }
            else
            {
                GetNextWave();
            }
        }

    }

    public void SpawnBoss()
    {
        GameObject enemy = currentWave.BossPrefabs;
        Transform chosenSP = GenerateRandomSpawnPoint(bossSpawnPoint);

        GameObject spawned = Instantiate(enemy, chosenSP.position, Quaternion.identity);
        //spawned.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(chosenSP.position);
        roomNavMeshSurface.BuildNavMesh();
        currentEnemies.Add(spawned);
        bossSpawned = true;
    }

    IEnumerator SpawningCorutine()
    {
        canSpawnWave = false;
        for (int i = 0; i <= currentWave.numOfEnemies; i++)
        {
            Transform chosenSP = GenerateRandomSpawnPoint(spawnPoints);
            GameObject enemy = GenerateRandomEnemy();
            GameObject spawnedEnemy = Instantiate(enemy, chosenSP.position, Quaternion.identity);
            //enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(chosenSP.position);
            
            spawnedEnemy.GetComponent<EnemyController>().ownerRoom = this;
            roomNavMeshSurface.BuildNavMesh();
            currentEnemies.Add(spawnedEnemy);

            yield return new WaitForSeconds(currentWave.spawnDelay);
        }

        currentWave.isSpawningComplete = true;
    }
    public void GetNextWave()
    {
        if(currWaveIndex < waves.Count-1)
        {
            currWaveIndex++;
            currentWave = waves[currWaveIndex];
            canSpawnWave = true;
            //return currentWave;
        }
        else
        {
            OnRoomComplete?.Invoke();
        }
    }

    public void RemoveFromList(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(isActive)
        {
            SpawnWave();
        }
    }
}
