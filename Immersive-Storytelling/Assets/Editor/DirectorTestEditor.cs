using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DirectorScript))]
public class DirectorTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DirectorScript director = (DirectorScript)target;

        if (GUILayout.Button("Initial"))
        {
            director.ResetDirector();
        }

        if (GUILayout.Button("DayNight"))
        {
            if (director.InitialState is InitialState initState) {
                initState.OnButtonPress();
            }
        }
    }
}
