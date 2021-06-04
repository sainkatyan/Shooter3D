using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModel
{
    public int idAttacker;
    public int idWeapon;
    public float damage;

    public DamageModel(int _idAttacker, int _idWeapon, float _damage)
    {
        idAttacker = _idAttacker;
        idWeapon = _idWeapon;
        damage = _damage;
    }
}
