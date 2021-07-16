using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldBuilder))]
public class FieldEditor : Editor
{
    private FieldBuilder _instance;

    private void OnEnable()
    {
        _instance = (FieldBuilder)target;
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
