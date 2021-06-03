using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType { Automate, Manual };
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Settings", order = 51)]
public class WeaponSettings : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private float _timeFireRate;
    [SerializeField] private float _timeRecharge;
    [SerializeField] private float _attackDamage;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _countOfShoot; //shootgun
    [SerializeField]private ShootType _shootType; 

    public int Id
    {
        get
        {
            return _id;
        }
    }
    public float TimeFireRate
    {
        get
        {
            //скорострельность
            return _timeFireRate;
        }
    }
    public float TimeRecharge
    {
        get
        {
            //время перезарядки
            return _timeRecharge;
        }
    }
    public float AttackDamage
    {
        get
        {
            return _attackDamage;
        }
    }
    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }
    public int CountOfShoot
    {
        get
        {
            return _countOfShoot;
        }
    }

    public ShootType ShootType
    {
        get
        {
            return _shootType;
        }
    }
}
