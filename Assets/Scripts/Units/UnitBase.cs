using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, ISpawner, IFraction
{
    private SpawnPoint _spawnerPoint;
    private FractionUnit _fraction;
    public SpawnPoint SpawnerPoint
    {
        get
        {
            return _spawnerPoint;
        }
    }
    public FractionUnit  fraction
    {
        get
        {
            return _fraction;
        }
    }

    public void SetFraction(FractionUnit fraction)
    {
        _fraction = fraction;
    }

    public void SetSpawnerTransform(SpawnPoint spawnPoint)
    {
        _spawnerPoint = spawnPoint;
    }
}
