using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private InputController _inputController;
    public float _angularSpeed = 500f;
    public void Init(InputController inputController)
    {
        _inputController = inputController;
    }
    void Update()
    {
        var rotation = _inputController.RotateInput() * _angularSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0f, rotation.x, 0f);
    }
}
