using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    internal InputController inputController;

    public UnitMovement movement;
    public UnitShooting shooting;
    public CameraMovement cameraMovement;

    private void Awake()
    {
        inputController = gameObject.AddComponent<InputController>();

        movement.Init(inputController, cameraMovement);
        cameraMovement.Init(inputController);
    }
}
