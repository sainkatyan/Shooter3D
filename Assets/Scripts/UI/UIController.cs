using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private GameObject _windiwRebirthing;
    [SerializeField] private Camera _uiCamera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisableSettings();
    }
    public void EnableSettings()
    {
        _uiCamera.gameObject.SetActive(true);
        _windiwRebirthing.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableSettings()
    {
        _uiCamera.gameObject.SetActive(false);
        _windiwRebirthing.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReBirthPlayer()
    {
        Spawner.instance.SpawnPlayer();
        DisableSettings();
    }

    public void ResumeGame()
    {
        Spawner.instance.SpawnPlayer();
    }
}
