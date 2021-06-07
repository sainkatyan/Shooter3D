using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamagable, ISpawner
{
    public float health;
    public SpawnPoint _spawnerPoint; 
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
        if (health <= 0f)
        {
            Death(model);
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
