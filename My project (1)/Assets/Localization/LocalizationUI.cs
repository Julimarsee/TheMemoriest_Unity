using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
internal class LocalizationUI : MonoBehaviour
{
    [SerializeField] private string key;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (text != null && LanguageManager.Instance != null)
            text.text = LanguageManager.Instance.Get(key);
    }
}