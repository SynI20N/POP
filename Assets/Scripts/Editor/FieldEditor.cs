using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Field))]
public class FieldEditor : Editor
{
    private Field _instance;

    private void OnEnable()
    {
        _instance = (Field)target;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Rebuild"))
        {
            _instance.Rebuild();
        }
        DrawDefaultInspector();
    }
}
