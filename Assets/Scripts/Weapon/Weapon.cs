using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseWeapon
{
    [SerializeField]
    private WeaponSettings _weaponSetting;

    private int _id;
    private float _timeFireRate;
    private float _timeRecharge;
    private float _attackDamage;
    private Sprite _icon;
    private int _countOfShoot; 
    private ShootType _shootType;

    private bool _isShooting = false;
    private float _timer;

    public LayerMask ignoreLayer;
    public Transform camPivot;
    private Ray Ray { get => new Ray(camPivot.transform.position + camPivot.transform.forward, camPivot.transform.forward); }

    private void Start()
    {
        camPivot = Camera.main.transform;

        SetSettings();
    }
    private void SetSettings()
    {
        _id = _weaponSetting.Id;
        _timeFireRate = _weaponSetting.TimeFireRate;
        _timeRecharge = _weaponSetting.TimeRecharge;
        _attackDamage = _weaponSetting.AttackDamage;
        _icon = _weaponSetting.Icon;
        _countOfShoot = _weaponSetting.CountOfShoot;
        _shootType = _weaponSetting.ShootType;
    }

    private void Update()
    {
        if (_isShooting == false) return;
        if (_timer <= _timeFireRate)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            switch (_weaponSetting.ShootType)
            {
                case ShootType.Automate:
                    AutomateShot();
                    //VisualiseAutomateShot();
                    break;
                case ShootType.Manual:
                    AutomateShot();
                    break;
            }
            _timer = 0f;
        }
    }
    private void AutomateShot()
    {
        Debug.Log("SHOT");
        RaycastHit hit;
        var hitchek = Physics.Raycast(Ray, out hit, 1000f, ~ignoreLayer);
        Debug.DrawRay(camPivot.transform.position, camPivot.transform.forward, Color.red, ~ignoreLayer);
        if (hitchek)
        {
            Debug.Log("hit: " + hit.distance + " " + hit.collider.gameObject.name);
        }
    }
    //private void VisualiseAutomateShot()

    public override void StartShoot()
    {
        _timer = float.MaxValue;
        _isShooting = true;
    }

    public override void StopShoot()
    {
        _timer = 0f;
        _isShooting = false;
    }
}
