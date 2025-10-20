using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DirectorScript))]
public class DirectorTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DirectorScript director = (DirectorScript)target;

        if (GUILayout.Button("Reset"))
        {
            director.ResetDirector();
        }

        if (GUILayout.Button("Next State"))
        {
            director.NextState();
        }
    }
}
