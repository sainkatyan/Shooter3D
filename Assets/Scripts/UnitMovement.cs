using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody _rigidUnit;
    public float speed = 10f;

    private InputController _inputController;
    private CameraMovement _cameraMovement;

    private void Start()
    {
        _rigidUnit = GetComponent<Rigidbody>();
    }

    public void Init(InputController inputController, CameraMovement cameraMovement)
    {
        _inputController = inputController;
        _cameraMovement = cameraMovement;
    }

    void FixedUpdate()
    {
        var moveInput = _inputController.MoveInput();
        var targetPosition = _cameraMovement.transform.forward * moveInput.y + _cameraMovement.transform.right * moveInput.x;
        targetPosition *= Time.fixedDeltaTime * speed;
        _rigidUnit.MovePosition(transform.position + targetPosition);

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, _cameraMovement.transform.rotation.eulerAngles.y, 0f);

        //_rigidUnit.AddForce(_cameraMovement.transform.forward * (moveInput.y * Time.fixedDeltaTime * _speed), ForceMode.Force);
        //_rigidUnit.AddForce(_cameraMovement.transform.right * (moveInput.x * Time.fixedDeltaTime * _speed), ForceMode.Force);
    }
}
