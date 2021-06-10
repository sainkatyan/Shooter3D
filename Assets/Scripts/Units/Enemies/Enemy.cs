using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [SerializeField]private WeaponController weaponController;
    public Collider unitTargetCollider;
    //public UnitBase unitTarget;

    private void Awake()
    {
        weaponController.Init(this);
    }
    private void Start()
    {
        IsDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitBase unit = null;
        other.transform.TryGetComponent<UnitBase>(out unit);
        if (unit != null)
        {
            if (unit.fraction != this.fraction)
            {
                unitTargetCollider = other;
                //unitTarget = GetComponent<UnitBase>();
                weaponController.StartShoot();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopShooting(other);
    }

    private void StopShooting(Collider other)
    {
        if (other == unitTargetCollider)
        {
            unitTargetCollider = null;
            weaponController.StopShoot();
        }
    }

    private void Update()
    {
        if (unitTargetCollider != null)
        {
            transform.LookAt(unitTargetCollider.transform);
        }
        else
        {
            weaponController.StopShoot();
        }
    }
}
