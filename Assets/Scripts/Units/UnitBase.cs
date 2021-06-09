using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, ISpawner, IFraction
{
    private SpawnPoint _spawnerPoint;
    private FractionUnit _fraction;
    private string _unitBaseName;
    private bool _isDead;
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
    public string UnitBaseName
    {
        get
        {
            return _unitBaseName;
        }
        set
        {
            _unitBaseName = value;
        }
    }
    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            _isDead = value;
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
