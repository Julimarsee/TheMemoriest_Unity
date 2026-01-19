using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpeakerTag), typeof(MethodTag), typeof(CooldownTag))]
public class Tags : MonoBehaviour
{
    private readonly Dictionary<string, ITag> _map = new ();

    private void Start()
    {
        _map.Add("speaker", GetComponent<SpeakerTag>());
        _map.Add("method", GetComponent<SpeakerTag>());
        _map.Add("cooldown", GetComponent<SpeakerTag>());
    }

    public ITag GetValue(string key)
    {
        return _map.GetValueOrDefault(key);
    }
}
