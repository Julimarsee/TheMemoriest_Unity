using UnityEngine;

public class LanguageManager : MonoBehaviour {
    public static LanguageManager Instance { get; private set; }

    [Header("Language Files")]
    public LocalizationData defaultLanguage;
    public LocalizationData russian;
    public LocalizationData english;
    //можно будет добавить потом в этом случае в Assets/ создать Localization -> New Language File
    //в LanguageManager на сцене в скрипт установить новый €зык

    private LocalizationData currentData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadLanguage()
    {
        string saved = PlayerPrefs.GetString("Language", "");
        SystemLanguage lang = SystemLanguage.Russian;

        if (!string.IsNullOrEmpty(saved))
        {
            if (System.Enum.TryParse(saved, out SystemLanguage parsed))
                lang = parsed;
        }
        else
        {
            lang = Application.systemLanguage;
        }

        SetLanguage(lang);
    }

    public void SetLanguage(SystemLanguage language)
    {
        currentData = language switch
        {
            SystemLanguage.Russian => russian ?? defaultLanguage,
            SystemLanguage.English => english ?? defaultLanguage,
            //ƒобавл€ть сюда
            _ => defaultLanguage,
        };

        PlayerPrefs.SetString("Language", language.ToString());
        PlayerPrefs.Save();

        Debug.Log($"язык: {language} -> {currentData.name}");

        var uiElements = Object.FindObjectsByType<LocalizationUI>(FindObjectsSortMode.None);
        //метод FindObjectsOfType<>() устарел, можно заменить этим

        foreach (var ui in uiElements) ui.UpdateText();
    }

    public string Get(string key)
    {
        if (currentData == null) return key;
        return currentData.GetText(key);
    }

}
