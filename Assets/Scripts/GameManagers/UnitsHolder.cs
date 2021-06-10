using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHolder : MonoBehaviour
{
    private static UnitsHolder instance;
    public static List<UnitBase> _units;
    private void Awake()
    {
        instance = this;
        _units = new List<UnitBase>();
    }
    public static void AddUnit(UnitBase unit)
    {
        _units.Add(unit);
    }

    public static void RemoveUnit(UnitBase unit)
    {
        _units.Remove(unit);
        Destroy(unit.gameObject);
    }

    public static void UpdateMainCameraForUnits()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].GetComponent<Health>().FindMainCamera();
        }
    }
}
