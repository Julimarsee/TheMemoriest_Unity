using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Localization", menuName = "Localization/New Language File")]
public class LocalizationData : ScriptableObject
{
    public List<string> keys = new()
    {
        "hint_movement",
        "menu_play",
        "hint_interact"
        //Сюда добавлять новые ключи, а в Unity перевод
        //Если добавляешь ключ в одном языке в Unity- в другом его нет
    };

    [System.Serializable]
    public class Translation
    {
        public string key;
        [TextArea]public string value;

        public Translation(string key, string value = "")
        {
            this.key = key;
            this.value = value;
        }
    }

    public List<Translation> translations = new List<Translation>();

    public void CreateKeys()
    {
        Clear();

        foreach (var key in keys)
        {
            translations.Add(new Translation(key));
        }
    }

    public string GetText(string key)
    {
        Translation t = translations.Find(x => x.key == key);
        if (t == null)
        {
            Debug.LogWarning($"Перевод для ключа '{key}' не найден!");
            return key;
        }
        return t.value;
    }

    public void Clear() => translations.Clear();
}
