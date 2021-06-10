using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;
    private UnitBase _parentUnit;

    private int idWeaponSlot = 0;
    public List<BaseWeapon> weaponSlotes;
    public BaseWeapon EquippedWeapon { get; private set; }

    private void Awake()
    {
        EquipWeapon(weaponSlotes[0]);
    }

    public void Init(UnitBase unitBase)
    {
        _parentUnit = unitBase;
    }

    private void EquipWeapon(BaseWeapon newWeapon)
    {
        if (EquippedWeapon != null)
        {
            Destroy(EquippedWeapon.gameObject);
        }

        EquippedWeapon = Instantiate(newWeapon, _weaponPivot.position, _weaponPivot.rotation);
        EquippedWeapon.transform.SetParent(_weaponPivot);
        EquippedWeapon.SetInfoBaseUnit(_parentUnit);
    }

    public void StartShoot()
    {
        EquippedWeapon.StartShoot();
    }
    public void StopShoot()
    {
        EquippedWeapon.StopShoot();
    }
    public void ChangeSlot()
    {
        idWeaponSlot++;
        if (idWeaponSlot >= weaponSlotes.Count)
        {
            idWeaponSlot = 0;
        }
        EquipWeapon(weaponSlotes[idWeaponSlot]);
    }
}
