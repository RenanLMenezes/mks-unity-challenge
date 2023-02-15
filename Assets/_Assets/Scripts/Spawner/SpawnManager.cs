using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float timer = 3f;
    public List<GameObject> spawner;

    private void Start()
    {
        StartCoroutine(DoSpawnEnemy());
    }

    IEnumerator DoSpawnEnemy()
    {
        yield return new WaitForSeconds(timer);
        var i = Random.Range(0, spawner.Count);
        EnemySpawner spawn = spawner[i].GetComponent<EnemySpawner>();
        spawn.SpawnRandomEnemy();
        StartCoroutine(DoSpawnEnemy());
    }
}
