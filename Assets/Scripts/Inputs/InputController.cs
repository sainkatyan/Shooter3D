using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector] public Unit unit;
    public void InitializeController(Unit baseController)
    {
        unit = baseController;

        InputEvents.Event_StartShoot.AddListener(StartShoot);
        InputEvents.Event_StopShoot.AddListener(StopShoot);

        InputEvents.Event_StartChangingWeapon.AddListener(ChangeWeapon);

        //need to be visible = true in menu
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public virtual Vector2 MoveInput()
    {
        return InputEvents.GetMoveInput();
    }
    public virtual Vector2 RotateInput()
    {
        return InputEvents.GetRotateInput();
    }
    public virtual void StartShoot()
    {
        unit.shooting.StartShoot();
    }
    public virtual void StopShoot()
    {
        unit.shooting.StopShoot();
    }
    public virtual void ChangeWeapon()
    {
        unit.shooting.ChangeSlot();
    }
}
