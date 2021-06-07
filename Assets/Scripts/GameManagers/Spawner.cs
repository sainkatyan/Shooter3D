using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner instance;

    [SerializeField] private UnitBase _player;
    [SerializeField] private UnitBase _enemy;

    [SerializeField] private SpawnPoint[] _playerSpawnPoint;
    [SerializeField] private SpawnPoint[] _enemySpawnPoints;
    
    private const float rebirthTimer = 5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    private void SpawnPlayer()
    {
        SpawnBaseUnit(_player, _playerSpawnPoint[Random.Range(0, _playerSpawnPoint.Length - 1)]);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            SpawnBaseUnit(_enemy, _enemySpawnPoints[i]);
        }
    }

    public static void SpawnBaseUnit(UnitBase unitPerson, SpawnPoint spawnPoint)
    {
        var spawnerUnit = Instantiate(unitPerson, spawnPoint.transform.position, Quaternion.identity);
        spawnerUnit.SetSpawnerTransform(spawnPoint);

        UnitsHolder.AddUnit(spawnerUnit);
    }

    public static void KillEnemyUnit(UnitBase unitPerson)
    {
        UnitsHolder.RemoveUnit(unitPerson);

        SpawnPoint enemySpawnerPoint = unitPerson.SpawnerPoint;
        instance.StartCoroutine(RebirthEnemy(rebirthTimer, enemySpawnerPoint));
        Destroy(unitPerson.gameObject);
    }

    public static IEnumerator RebirthEnemy(float time, SpawnPoint spawnPoint)
    {
        Debug.Log("REBIRTH ENEMY");
        yield return new WaitForSeconds(time);
        SpawnBaseUnit(instance._enemy, spawnPoint);
    }
}
