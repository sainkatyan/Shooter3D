using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [SerializeField] private UnitBase _player;
    [SerializeField] private UnitBase _enemy;

    [SerializeField] private SpawnPoint[] _playerSpawnPoint;
    [SerializeField] private SpawnPoint[] _enemySpawnPoints;
    
    private const float rebirthTimer = 5f;
    private static int index = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    public void SpawnPlayer()
    {
        SpawnBaseUnit(_player, _playerSpawnPoint[Random.Range(0, _playerSpawnPoint.Length - 1)], FractionUnit.Blue);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            SpawnBaseUnit(_enemy, _enemySpawnPoints[i], FractionUnit.Red);
        }
    }

    public static void SpawnBaseUnit(UnitBase unitPerson, SpawnPoint spawnPoint, FractionUnit fraction)
    {
        var spawnerUnit = Instantiate(unitPerson, spawnPoint.transform.position, Quaternion.identity);
        spawnerUnit.SetSpawnerTransform(spawnPoint);
        spawnerUnit.SetFraction(fraction);
        spawnerUnit.UnitBaseName = fraction.ToString() + " " + index++;

        UnitsHolder.AddUnit(spawnerUnit);
    }

    public static void KillEnemyUnit(UnitBase unitPerson, DamageModel damageModel)
    {
        UnitsHolder.RemoveUnit(unitPerson);
        SpawnPoint spawnerPoint = unitPerson.SpawnerPoint;

        switch (unitPerson.fraction)
        {
            case FractionUnit.Neutral:
                break;
            case FractionUnit.Blue:
                EnableUISettings();
                break;
            case FractionUnit.Red:
                instance.StartCoroutine(RebirthEnemy(rebirthTimer, spawnerPoint));
                break;
        }
        Destroy(unitPerson.gameObject);
    }

    public static IEnumerator RebirthEnemy(float time, SpawnPoint spawnPoint)
    {
        yield return new WaitForSeconds(time);
        SpawnBaseUnit(instance._enemy, spawnPoint, FractionUnit.Red);
    }

    public static void EnableUISettings()
    {
        Cursor.visible = true;
        UIController.instance.EnableSettings();
    }
}
