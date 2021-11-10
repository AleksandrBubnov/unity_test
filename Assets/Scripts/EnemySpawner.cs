using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] int _enemyCount;
    [SerializeField] int _spawnTime;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            yield return new WaitForSeconds(_spawnTime);
            Vector3 pos = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            Instantiate(_enemyPrefab, pos, Quaternion.identity);
        }
    }
}
