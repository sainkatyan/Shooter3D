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

    private float _minAmplituda = 0.1f;
    private float _multiplyForce = 0.1f;
    public float _addForceThrowingTimer = 1f;
    private float _speedAddForceThrowingTimer = 0.85f;
    private bool _hitCollider = false;

    [SerializeField] private Granade granadeObj;

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
        Debug.Log("SUM " + sum + " one " + one + " two " + two);
      
        _addForceThrowingTimer = 1f;
    }
    protected override void SetSettings(WeaponSettings weaponSettings)
    {
        base.SetSettings(weaponSettings);

        granadeObj = Instantiate(granadeObj);
        DamageModel damageModel = new DamageModel(_idWeapon, _idWeapon, _damage);
        granadeObj.SetDamageModel = damageModel;
    }

    private void Update()
    {
        if (_isAiming)
        {
            _addForceThrowingTimer -= Time.deltaTime * _speedAddForceThrowingTimer;
            if (_addForceThrowingTimer <= 0f)
            {
                _addForceThrowingTimer = 0f;
            }

            Vector3 one;
            one = firePivot.forward;

            Vector3 two;
            two = new Vector3(0f, Mathf.Cos(_startAngle), 0f);

            Vector3 sum = (one + two).normalized;

            _lineRenderer.positionCount = _lineRendererSegments;
            Vector3 nextPos = firePivot.position;

            float sin = (Mathf.Sin(_addForceThrowingTimer) + 1f) * 0.5f; 
            sin *= _multiplyForce + _minAmplituda;

            for (int i = 0; i < _lineRendererSegments; i++)
            {
                Vector3 prevPos = nextPos;
                _lineRenderer.SetPosition(i, nextPos);

                two = two + new Vector3(0f, -sin, 0f); //y = -9.81 * velocity
                sum = (one + two).normalized;

                nextPos += sum;
                Shoot(prevPos, nextPos);

                if (_hitCollider)
                {
                    _lineRenderer.positionCount = i + 1;
                    break;
                }
            }
        }
    }

    private void Shoot(Vector3 startPos, Vector3 endPos)
    {
        RaycastHit hit;
        _hitCollider = Physics.Raycast(startPos, endPos - startPos, out hit, Vector3.Distance(endPos, startPos));
        if (_hitCollider == true)
        {
            granadeObj.transform.position = hit.point;
            Debug.DrawLine(startPos, hit.point, Color.red, 1f);
        }
        else
        {
            Debug.DrawLine(startPos, endPos, Color.blue, 1f);
        }
    }

    private void ActivateGranade()
    {
        if (granadeObj != null)
        {
            granadeObj.ExplosionDamage(granadeObj.transform.position);
        }
    }

    public override void StartShoot()
    {
        _isAiming = true;
        _lineRenderer.enabled = true;
        _addForceThrowingTimer = 1f;
    }
    public override void StopShoot()
    {
        _lineRenderer.enabled = false;
        _isAiming = false;
        ActivateGranade();
    }
}
