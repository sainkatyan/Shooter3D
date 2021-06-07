using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : UnitBase
{
    internal InputController inputController;

    public UnitMovement unitMovement;
    public WeaponController shooting;
    public CameraMovement cameraMovement;

    public Health health;
    public Fraction unitFraction;

    private void Awake()
    {
        inputController = gameObject.AddComponent<InputController>();
        inputController.InitializeController(this);

        unitMovement.Init(inputController, cameraMovement);
        cameraMovement.Init(inputController, unitMovement);

        health = GetComponent<Health>();
    }
}
