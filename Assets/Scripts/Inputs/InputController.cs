using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector] public Unit cc;
    public void InitializeController(Unit baseController)
    {
        cc = baseController;

        InputEvents.Event_StartShoot.AddListener(StartShoot);
    }
    public virtual Vector2 MoveInput()
    {
        return InputEvents.GetMoveInput();
    }
    public virtual Vector2 RotateInput()
    {
        return InputEvents.GetRotateInput();
    }
    protected virtual void StartShoot()
    {
        cc.shooting.StartShoot();
    }
    protected virtual void StopShoot()
    {
        cc.shooting.StopShoot();
    }
}
