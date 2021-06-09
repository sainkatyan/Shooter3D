using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    public static CharacterControl InputActions;

    public static UnityEvent Event_StartShoot;
    public static UnityEvent Event_StopShoot;

    public static UnityEvent Event_StartChangingWeapon;

    public static UnityEvent Event_Escape;

    private void Awake()
    {
        InitializeEvents();
        InitializeController();
    }

    private void OnDisable()
    {
        InputActions.Dispose();
        DestroyEvents();
    }

    private void InitializeEvents()
    {
        Event_StartShoot = new UnityEvent();
        Event_StopShoot = new UnityEvent();

        Event_StartChangingWeapon = new UnityEvent();

        Event_Escape = new UnityEvent();
    }

    private void DestroyEvents()
    {
        Event_StartShoot.RemoveAllListeners();
        Event_StopShoot.RemoveAllListeners();

        Event_StartChangingWeapon.RemoveAllListeners();
        Event_Escape.RemoveAllListeners();
    }

    protected virtual void InitializeController()
    {
        InputActions = new CharacterControl();
        InputActions.Enable();

        InputActions.Character.Shoot.started += StartShoot;
        InputActions.Character.Shoot.canceled += StopShoot;

        InputActions.Character.ChangeWeapon.started += StartChangingWeapon;
        InputActions.Global.Escape.started += Escape;
    }

    //for menu
    public static void DisableCharacterInput()
    {
        InputActions.Character.Disable();
    }

    public static void EnableCharacterInput()
    {
        InputActions.Character.Enable();
    }

    public static Vector2 GetMoveInput()
    {
        return InputActions.Character.Move.ReadValue<Vector2>();
    }

    public static Vector2 GetRotateInput()
    {
        return InputActions.Character.Rotate.ReadValue<Vector2>();
    }

    public static void StartShoot(InputAction.CallbackContext context)
    {
        Event_StartShoot?.Invoke();
    }
    public static void StopShoot(InputAction.CallbackContext context)
    {
        Event_StopShoot?.Invoke();
    }
    public static void StartChangingWeapon(InputAction.CallbackContext context)
    {
        Event_StartChangingWeapon?.Invoke();
    }
    public static void Escape(InputAction.CallbackContext context)
    {
        Event_Escape?.Invoke();
    }
}
