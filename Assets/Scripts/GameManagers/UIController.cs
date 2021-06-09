using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private GameObject _windiwRebirthing;

    private void Awake()
    {
        instance = this;
    }

    public void EnableSettings()
    {
        _windiwRebirthing.SetActive(true);
        Cursor.visible = true;
    }
    public void ReBirthPlayer()
    {
        Spawner.instance.SpawnPlayer();
        _windiwRebirthing.SetActive(false);
        Cursor.visible = false;
    }

    public void ResumeGame()
    {
        Spawner.instance.SpawnPlayer();
    }
}
