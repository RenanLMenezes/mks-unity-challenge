using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefab;

    public void SpawnRandomEnemy()
    {
        int i = Random.Range(0, enemyPrefab.Count);
        Instantiate(enemyPrefab[i], transform.position, transform.rotation);
    }
}
