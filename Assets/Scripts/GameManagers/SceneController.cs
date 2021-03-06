using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        if (LocalizationManager.IsReady)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
