using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    public string key;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        LocalizationManager.LocalizationUpdateEvent.AddListener(UpdateLocalization);
    }

    private void UpdateLocalization()
    {
        text.text = LocalizationManager.GetLocalizedValue(key);
    }
}