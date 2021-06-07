using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField]private float _speed = 10f;
    private Rigidbody _rigidUnit;

    private InputController _inputController;
    private CameraMovement _cameraMovement;

    public GameObject playeMesh;

    private void Start()
    {
        _rigidUnit = GetComponent<Rigidbody>();

        
    }

    public void Init(InputController inputController, CameraMovement cameraMovement)
    {
        _inputController = inputController;
        _cameraMovement = cameraMovement;
    }

    private void FixedUpdate()
    {
        var moveInput = _inputController.MoveInput();

        Vector3 dir = new Vector3(Mathf.Sin(_cameraMovement.transform.eulerAngles.y * Mathf.Deg2Rad), 0f, Mathf.Cos(_cameraMovement.transform.eulerAngles.y * Mathf.Deg2Rad));
        var targetPosition = dir * moveInput.y + _cameraMovement.transform.right * moveInput.x;

        targetPosition *= Time.fixedDeltaTime * _speed;
        _rigidUnit.MovePosition(transform.position + targetPosition);

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void RotateMesh()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        playeMesh.transform.localRotation = Quaternion.Euler(0f, _cameraMovement.transform.localRotation.eulerAngles.y, 0f);
    }
}
