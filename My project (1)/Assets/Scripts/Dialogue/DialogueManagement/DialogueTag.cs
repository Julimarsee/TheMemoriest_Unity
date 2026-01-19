using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tags))]
public class DialogueTag : MonoBehaviour
{
    private Tags _tags;

    private void Awake()
    {
        _tags = GetComponent<Tags>();
    }

    public void HandleTags(List<string> tags)
    {
        if(tags.Count == 0 ) return;

        foreach (string tagValue in tags)
        {
            string[] keyTag = tagValue.Split(":");

            if(keyTag.Length != 2)
            {
                throw new ArgumentException("Неправильное оформление тега, просьба исправить");
            }

            string key = keyTag[0].Trim();
            string value = keyTag[1].Trim();

            _tags.GetValue(key).Calling(value);
        }
    }
}
