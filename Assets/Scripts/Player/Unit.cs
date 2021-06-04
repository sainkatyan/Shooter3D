using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    internal InputController inputController;

    public UnitMovement unitMovement;
    public WeaponController shooting;
    public CameraMovement cameraMovement;

    private float hp = 100f;

    private void Awake()
    {
        inputController = gameObject.AddComponent<InputController>();
        inputController.InitializeController(this);

        unitMovement.Init(inputController, cameraMovement);
        cameraMovement.Init(inputController, unitMovement);
    }
    public void Damage(DamageModel model)
    {
        hp -= model.damage;
        if (hp <= 0f)
        {
            Death(model);
        }
    }

    private void Death(DamageModel damageModel)
    {
        Debug.Log("DEATH");
    }
}
