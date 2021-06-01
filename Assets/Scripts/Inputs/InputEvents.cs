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
    }

    private void DestroyEvents()
    {
        Event_StartShoot.RemoveAllListeners();
        Event_StopShoot.RemoveAllListeners();
    }

    protected virtual void InitializeController()
    {
        InputActions = new CharacterControl();
        InputActions.Enable();

        InputActions.Character.Shoot.started += StartShoot;
        InputActions.Character.Shoot.canceled += StopShoot;
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

    protected virtual void StartShoot(InputAction.CallbackContext context)
    {
        Event_StartShoot?.Invoke();
    }
    protected virtual void StopShoot(InputAction.CallbackContext context)
    {
        Event_StopShoot?.Invoke();
    }
}
