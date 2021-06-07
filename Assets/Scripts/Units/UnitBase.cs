using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamagable, ISpawner
{
    [SerializeField] private float health;
    private bool _isDead;
    private SpawnPoint _spawnerPoint;

    protected virtual void SetUnitSettings()
    {
        _isDead = false;
    }

    public SpawnPoint SpawnerPoint
    {
        get
        {
            return _spawnerPoint;
        }
    }
    public virtual void Damage(DamageModel model)
    {
        health -= model.damage;
        if (_isDead == false)
        {
            if (health <= 0f)
            {
                _isDead = true;
                Death(model);
            }
        }
    }

    public void SetSpawnerTransform(SpawnPoint spawnPoint)
    {
        _spawnerPoint = spawnPoint;
    }

    private void Death(DamageModel damageModel)
    {
        Debug.Log("DEATH");
        Spawner.KillEnemyUnit(this);
    }
}
