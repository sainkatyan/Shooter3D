using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public BaseWeapon _equippedWeaponPrefub = null;
    [SerializeField] private Transform _weaponPivot;

    private int idWeaponSlot = 0;
    public List<BaseWeapon> weaponSlotes;
    public BaseWeapon EquippedWeapon { get; private set; }

    private void Awake()
    {
        _equippedWeaponPrefub = weaponSlotes[idWeaponSlot];
        EquipWeapon(_equippedWeaponPrefub);
    }
    private void EquipWeapon(BaseWeapon newWeapon)
    {
        if (EquippedWeapon != null)
        {
            Destroy(EquippedWeapon.gameObject);
        }

        EquippedWeapon = Instantiate(newWeapon, _weaponPivot.position, _weaponPivot.rotation);
        EquippedWeapon.transform.SetParent(_weaponPivot);
        _equippedWeaponPrefub = EquippedWeapon;
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
