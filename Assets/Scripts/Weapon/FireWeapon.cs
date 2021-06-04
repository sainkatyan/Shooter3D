using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : BaseWeapon
{
    [SerializeField] private WeaponSettings _weaponSetting;
    private float distanceOfDamage = 40f;
    [SerializeField] private Transform firePivot;

    private int _idWeapon;
    private float _timeFireRate;
    private float _timeRecharge;
    private float _damage;
    private Sprite _icon;
    private int _countOfShoot; 
    private ShootType _shootType;
    private int _bulletsInMagazine;

    private bool _isShooting = false;
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
        SetSettings();
        GetStartSettings();
    }
    private void SetSettings()
    {
        _idWeapon = _weaponSetting.Id;
        _timeFireRate = _weaponSetting.TimeFireRate;
        _timeRecharge = _weaponSetting.TimeRecharge;
        _damage = _weaponSetting.AttackDamage;
        _icon = _weaponSetting.Icon;
        _countOfShoot = _weaponSetting.CountOfShoot;
        _shootType = _weaponSetting.ShootType;
        _bulletsInMagazine = _weaponSetting.BulletsInMagazine;
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
        Debug.Log("Automate SHOT");
        RaycastHit hit;
        var hitchek = Physics.Raycast(firePivot.position, firePivot.forward, out hit, distanceOfDamage, ~ignoreLayer);
        if (hitchek)
        {
            Debug.Log("hit");
            CreateVisualBullet(hit.point);
            CheckIUnit(hit);
            //Debug.DrawLine(firePivot.position, hit.point, Color.red, 1f);
        }
        else
        {
            var targetBulletPos = firePivot.transform.forward * distanceOfDamage;
            CreateVisualBullet(targetBulletPos);
            //Debug.DrawRay(firePivot.position, targetBulletPos, Color.blue, 3f);
        }
        _fireTimer = _timeFireRate;
    }
    private void ManualShot()
    {
        Debug.Log("Manual SHOT");
        Vector3 dirAndDistanceOfSphere = firePivot.forward * _zoneOfDamage;
        for (int i = 0; i < _countOfShoot; i++)
        {
            var bulletTargetDir = SetBulletTargetDir(dirAndDistanceOfSphere);

            RaycastHit hit;
            var hitchek = Physics.Raycast(firePivot.position, bulletTargetDir, out hit, distanceOfDamage, ~ignoreLayer);
            if (hitchek)
            {
                CreateVisualBullet(hit.point);
                CheckIUnit(hit);
                //Debug.DrawLine(firePivot.position, hit.point, Color.red, 1f);
            }
            else
            {
                var targetBulletPos = firePivot.position + bulletTargetDir * distanceOfDamage;
                CreateVisualBullet(targetBulletPos);
                //Debug.DrawLine(firePivot.position, targetBulletPos, Color.blue, 3f);
            }
        }

        _fireTimer = _timeFireRate;
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
        IUnit damageInterface = null;
        tempHit.transform.TryGetComponent<IUnit>(out damageInterface);

        if (damageInterface != null)
        {
            DamageModel damageModel = new DamageModel(idAttacker, _idWeapon, _oneBulletDamage);
            damageInterface.Damage(damageModel);
        }
    }
    public override void StartShoot()
    {
        _isShooting = true;
    }
    public override void StopShoot()
    {
        _isShooting = false;
    }
}
