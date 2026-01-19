#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationData))]
public class LocalizationDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalizationData data = (LocalizationData)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("Создать доступные ключи"))
        {
            data.CreateKeys();
            EditorUtility.SetDirty(data); 
        }
    }
}
#endif