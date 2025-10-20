using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DayNightCycle))]
public class TestDayNightCycleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DayNightCycle dayNightCycleScript = (DayNightCycle)target;

        if (GUILayout.Button("Start StartUp"))
        {
            dayNightCycleScript.StartSpeedUp();
        }

        if (GUILayout.Button("Start SlowDown"))
        {
            dayNightCycleScript.StartSlowDown();
        }
    }
    
}
