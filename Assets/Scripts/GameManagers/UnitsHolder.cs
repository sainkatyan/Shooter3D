using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHolder : MonoBehaviour
{
    private static UnitsHolder instance;
    public List<UnitBase> _units;
    private void Awake()
    {
        instance = this;
        _units = new List<UnitBase>();
    }

    public static void AddUnit(UnitBase unit)
    {
        instance._units.Add(unit);
    }

    public static void RemoveUnit(UnitBase unit)
    {
        instance._units.Remove(unit);
        Destroy(unit.gameObject);
    }
}
