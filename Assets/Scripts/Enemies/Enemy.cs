using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    [SerializeField]private WeaponController weaponController;
    private Collider unitTarget;

    private void OnTriggerEnter(Collider other)
    {
        UnitBase unit = null;
        other.transform.TryGetComponent<UnitBase>(out unit);
        if (unit != null)
        {
            if (unit.fraction != this.fraction)
            {
                unitTarget = other;
                weaponController.StartShoot();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == unitTarget)
        {
            unitTarget = null;
            weaponController.StopShoot();
        }
    }

    private void Update()
    {
        if (unitTarget != null)
        {
            transform.LookAt(unitTarget.transform);
        }
    }
}
