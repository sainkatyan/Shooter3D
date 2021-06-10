using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private float _lookSpeed = 1000f;

    private InputController _inputController;
    private UnitMovement _unitMovement;
    public void Init(InputController inputController, UnitMovement unitMovement)
    {
        _inputController = inputController;
        _unitMovement = unitMovement;
    }
    private void Update()
    {
        var rotation = _inputController.RotateInput() * _lookSpeed * Time.deltaTime;
        transform.localRotation *= Quaternion.Euler(rotation.y, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, rotation.x, 0f);

        float xAngel = transform.localRotation.eulerAngles.x;
        AngelCorrection(xAngel);
        transform.localRotation = Quaternion.Euler(xAngel, transform.localRotation.eulerAngles.y, 0f);

        _unitMovement.RotateMesh();
    }

    private float AngelCorrection(float angle)
    {
        if (angle > 180f)
        {
            if (angle < 280f)
            {
                angle = 280f;
            }
        }
        else
        {
            if (angle > 80f)
            {
                angle = 80f;
            }
        }
        return angle;
    }
}
