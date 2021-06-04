using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IUnit
{
    private int idAttacker = 1;
    [SerializeField]
    private float hp = 50f; 
    public void Damage(DamageModel model)
    {
        hp -= model.damage;
        if (hp <= 0f)
        {
            Death(model);
        }
    }

    private void Death(DamageModel damageModel)
    {
        Debug.Log("DEATH");
        Destroy(gameObject);
    }
}
