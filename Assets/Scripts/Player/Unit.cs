using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    internal InputController inputController;

    public UnitMovement unitMovement;
    public WeaponController shooting;
    public CameraMovement cameraMovement;

    private void Awake()
    {
        inputController = gameObject.AddComponent<InputController>();
        inputController.InitializeController(this);

        unitMovement.Init(inputController, cameraMovement);
        cameraMovement.Init(inputController, unitMovement);
    }
    public void Kill()
    {
        Debug.Log("dead");
    }
}
