using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelDataSO))]
public class LevelDataSOEditor : Editor
{
    LevelDataSO levelDataSO;

    void OnEnable()
    {
        levelDataSO = (LevelDataSO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save data as json"))
        {
            Util.FileOperations.Save(levelDataSO.pLevelPack, Constants.LEVELDATAFILE, false);
        }
    }
}
