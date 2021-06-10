using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : BaseWeapon
{
    [SerializeField] private WeaponSettings _weaponSettings;
    [SerializeField] private Transform firePivot;

    private LineRenderer _lineRenderer;
    private bool _isAiming = false;

    private float _startAngle = 25f;
    private int _lineRendererSegments = 30;

    private float _maxDistance = 5f;
    private float _minDistance = 0.1f;

    private float _addForceThrowingTimer = 0f;
    private float _speedAddForceThrowingTimer = 0.85f;
    private bool _hitCollider = false;
    private float _fireTimer = 0f;

    [SerializeField] private GranadeBullet _granadePrefub;
    private GranadeBullet _granadeBullet;

    public List<Vector3> targetPositions = new List<Vector3>();
    public LayerMask ignoreLayer;

    private void Start()
    {
        SetSettings(_weaponSettings);

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;

        Vector3 one = new Vector3(1f, 0f, 0f);
        one = firePivot.forward;
        Vector3 two = new Vector3(0f, 0f, 1f);
        two = new Vector3(0f, Mathf.Cos(_startAngle), 0f);
        Vector3 sum = (one + two).normalized;
      
        _fireTimer = _weaponSettings.TimeRecharge;
    }
    protected override void SetSettings(WeaponSettings weaponSettings)
    {
        base.SetSettings(weaponSettings);
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        if (_isAiming)
        {
            _addForceThrowingTimer += Time.deltaTime * _speedAddForceThrowingTimer;
            if (_addForceThrowingTimer >= 1f)
            {
                _addForceThrowingTimer = 1f;
            }

            Vector3 originDir;
            originDir = firePivot.forward;

            Vector3 dirModificator;
            dirModificator = new Vector3(0f, Mathf.Cos(_startAngle), 0f);

            Vector3 sumDir = (originDir + dirModificator).normalized;

            _lineRenderer.positionCount = _lineRendererSegments;
            Vector3 nextPos = firePivot.position;

            float verticalBalysticDelta = GetBalysticDelta();

            targetPositions.Clear();

            for (int i = 0; i < _lineRendererSegments; i++)
            {
                Vector3 prevPos = nextPos;
                _lineRenderer.SetPosition(i, nextPos);
                targetPositions.Add(nextPos);

                dirModificator += new Vector3(0f, -verticalBalysticDelta, 0f); 
                sumDir = (originDir + dirModificator).normalized;

                nextPos += sumDir;
                Shoot(prevPos, nextPos);

                if (_hitCollider)
                {
                    _lineRenderer.positionCount = i + 1;
                    break;
                }
            }
        }
    }

    private float GetBalysticDelta()
    {
        float verticalBalysticDelta = 1f / (_addForceThrowingTimer * _maxDistance + _minDistance);
        return verticalBalysticDelta;
    }

    private void Shoot(Vector3 startPos, Vector3 endPos)
    {
        RaycastHit hit;
        _hitCollider = Physics.Raycast(startPos, endPos - startPos, out hit, Vector3.Distance(endPos, startPos), ~ignoreLayer, QueryTriggerInteraction.Ignore);

        if (_hitCollider == true)
        {
            _granadeBullet.transform.position = hit.point;
            //Debug.DrawLine(startPos, hit.point, Color.red, 1f);
        }
        else
        {
            //Debug.DrawLine(startPos, endPos, Color.blue, 1f);
        }
    }

    public override void StartShoot()
    {
        if (_weaponSettings.TimeRecharge > _fireTimer) return;

        _isAiming = true;
        _lineRenderer.enabled = true;
        _addForceThrowingTimer = 0f;

        _granadeBullet = Instantiate(_granadePrefub);
       
    }
    public override void StopShoot()
    {
        if (_granadeBullet != null)
        {
            _fireTimer = 0f;
            _lineRenderer.enabled = false;
            _isAiming = false;

            DamageModel damageModel = new DamageModel(_parentUnit.UnitBaseName, _idWeapon, _damage);

            _granadeBullet.Shoot(targetPositions, damageModel);
            _granadeBullet = null;
        }
    }
}
