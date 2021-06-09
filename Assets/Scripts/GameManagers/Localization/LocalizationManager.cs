using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public enum Languages { En, Rus}

public class LocalizationManager : MonoBehaviour
{
    internal InputController inputController;

    private static Dictionary<string, string> localizedText;

    private static bool _isReady = false;
    public static bool IsReady { get => _isReady; }

    public static string MissingTextString = "Localized text not found";

    public static UnityEvent LocalizationUpdateEvent;

    void Awake()
    {
        if (LocalizationUpdateEvent == null)
            LocalizationUpdateEvent = new UnityEvent();

        inputController = gameObject.AddComponent<InputController>();
        inputController.InitializeUIController(this);
    }

    private IEnumerator Start()
    {
        yield return null;
        UpdateLocalization(Languages.En);
    }

    public void OnClickLocalizationRus()
    {
        UpdateLocalization(Languages.Rus);
    }
    public void OnClickLocalizationEn()
    {
        UpdateLocalization(Languages.En);
    }
    public void UpdateLocalization(Languages language)
    {
        LoadLocalizedText(GetFileName(language));
        LocalizationUpdateEvent?.Invoke();
    }

    private string GetFileName(Languages language)
    {
        string fileName;

        switch (language)
        {
            case Languages.En:
                fileName = "gamedata_en.json";
                break;
            case Languages.Rus:
                fileName = "gamedata_ru.json";
                break;
            default:
                fileName = "gamedata_en.json";
                break;
        }
        return fileName;
    }

    private void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        _isReady = true;
    }


    public static string GetLocalizedValue(string key)
    {
        string result = MissingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

}