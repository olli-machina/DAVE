using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabSwapEditor : EditorWindow
{

    public GameObject prefab;

    [MenuItem("My Tools/Prefab Swap")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PrefabSwapEditor));
    }

    public void OnGUI()
    {
        SerializedObject serialized = new SerializedObject(this);
        SerializedProperty prefabsProperty = serialized.FindProperty("prefab");


        EditorGUILayout.LabelField("Prefab Swap", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(prefabsProperty, true);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
