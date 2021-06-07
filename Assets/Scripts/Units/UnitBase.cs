using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, ISpawner
{
    private SpawnPoint _spawnerPoint;

    public SpawnPoint SpawnerPoint
    {
        get
        {
            return _spawnerPoint;
        }
    }

    public void SetSpawnerTransform(SpawnPoint spawnPoint)
    {
        _spawnerPoint = spawnPoint;
    }
}
