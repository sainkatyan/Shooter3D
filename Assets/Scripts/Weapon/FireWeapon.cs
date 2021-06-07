﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : BaseWeapon
{
    [SerializeField] private WeaponSettings _weaponSetting;
    private float distanceOfDamage = 40f;
    [SerializeField] private Transform firePivot;
  
    //extra set settings
    private int _countOfShoot; 
    private ShootType _shootType;
    private int _bulletsInMagazine;

    private bool _isRecharging = false;
    private float _fireTimer;

    public LayerMask ignoreLayer;
    private float _zoneOfDamage = 10f;
    private int _currentBulletsInMagazine;
    private float _oneBulletDamage;
    private int idAttacker = 0;

    [SerializeField] private Bullet _bullet;

    private void Start()
    {
        SetSettings(_weaponSetting);
        GetStartSettings();
    }
    protected override void SetSettings(WeaponSettings weaponSettings)
    {
        base.SetSettings(weaponSettings);

        _countOfShoot = weaponSettings.CountOfShoot;
        _shootType = weaponSettings.ShootType;
        _bulletsInMagazine = weaponSettings.BulletsInMagazine;
    }

    private void GetStartSettings()
    {
        _currentBulletsInMagazine = _bulletsInMagazine;
        _fireTimer = _timeFireRate;
        _oneBulletDamage = _damage / _countOfShoot;
    }

    private void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(10, 10, 1000, 20), "currentCountOfShoot: " + _currentBulletsInMagazine);
    }

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        if (_isShooting == false) return;
        if (_isRecharging == true) return;

        if (_fireTimer <= 0f)
        {
            CheckBulletsInMagazine();
            switch (_weaponSetting.ShootType)
            {
                case ShootType.Automate:
                    AutomateShot();
                    break;
                case ShootType.Manual:
                    ManualShot();
                    break;
            }
        } 
    }

    #region Charging
    private void CheckBulletsInMagazine()
    {
        _currentBulletsInMagazine -= _countOfShoot;
        if (_currentBulletsInMagazine < _countOfShoot)
        {
            Reсharge();
            return;
        }
    }
    private void Reсharge()
    {
        _isShooting = false;
        _isRecharging = true;
        Debug.Log("RECHARGE");

        StartCoroutine(StartRecharging());
    }
    private IEnumerator StartRecharging()
    {
        yield return new WaitForSeconds(_timeRecharge);
        _currentBulletsInMagazine = _bulletsInMagazine;
        _isRecharging = false;
    }
    #endregion
    private void AutomateShot()
    {
        Ray ray = new Ray(firePivot.position, firePivot.forward);
        Shoot(ray);

        _fireTimer = _timeFireRate;
    }
    private void ManualShot()
    {
        Vector3 dirAndDistanceOfSphere = firePivot.forward * _zoneOfDamage;
        for (int i = 0; i < _countOfShoot; i++)
        {
            var bulletTargetDir = SetBulletTargetDir(dirAndDistanceOfSphere);
            Ray ray = new Ray(firePivot.position, bulletTargetDir);
            Shoot(ray);
        }

        _fireTimer = _timeFireRate;
    }

    private void Shoot(Ray ray)
    {
        RaycastHit hit;
        var hitchek = Physics.Raycast(ray, out hit, distanceOfDamage, ~ignoreLayer);
        if (hitchek)
        {
            CreateVisualBullet(hit.point);
            CheckIUnit(hit);
            //Debug.DrawLine(firePivot.position, hit.point, Color.red, 1f);
        }
        else
        {
            var targetBulletPos = firePivot.position + ray.direction * distanceOfDamage;
            CreateVisualBullet(targetBulletPos);
            //Debug.DrawLine(firePivot.position, targetBulletPos, Color.blue, 3f);
        }
    }
    private Vector3 SetBulletTargetDir(Vector3 tempDirAndDistanceOfSphere)
    {
        var randomizer = Random.insideUnitSphere;
        var bulletTargetDir = tempDirAndDistanceOfSphere + randomizer;
        return bulletTargetDir.normalized;
    }

    private void CreateVisualBullet(Vector3 bulletEndPos)
    {
        Bullet bullet = Instantiate(_bullet, firePivot.position, Quaternion.identity);
        bullet.Init(firePivot.position, bulletEndPos);
    }

    private void CheckIUnit(RaycastHit tempHit)
    {
        IDamagable damageInterface = null;
        tempHit.transform.TryGetComponent<IDamagable>(out damageInterface);

        if (damageInterface != null)
        {
            DamageModel damageModel = new DamageModel(idAttacker, _idWeapon, _oneBulletDamage);
            damageInterface.Damage(damageModel);
        }
    }
}
