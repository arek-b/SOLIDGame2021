using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerDefaultPosition))]
public class PlayerDefaultPositionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerDefaultPosition myScript = target as PlayerDefaultPosition;

        const string ButtonSetText = "Set current world pos. as default";
        const string ButtonRestoreText = "Restore position to saved position";

        if (GUILayout.Button(ButtonSetText))
        {
            Undo.RecordObject(myScript.transform, ButtonSetText);
            myScript.SetCurrentPositionAsDefault();
        }

        if (GUILayout.Button(ButtonRestoreText))
        {
            Undo.RecordObject(myScript.transform, ButtonRestoreText);
            myScript.RestorePosition();
        }
    }
}
