using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    protected int _idWeapon;
    protected float _timeFireRate;
    protected float _timeRecharge;
    protected float _damage;
    protected Sprite _icon;

    protected bool _isShooting = false;
    protected virtual void SetSettings(WeaponSettings weaponSetting)
    {
        _idWeapon = weaponSetting.Id;
        _timeFireRate = weaponSetting.TimeFireRate;
        _timeRecharge = weaponSetting.TimeRecharge;
        _damage = weaponSetting.AttackDamage;
        _icon = weaponSetting.Icon;
    }

    public abstract void StartShoot();
    public abstract void StopShoot();

    public abstract void GetInfoBaseUnit(string name);
}
