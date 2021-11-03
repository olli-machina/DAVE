using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabSwapEditor : EditorWindow
{

    public GameObject prefab;

    public GameObject[] selectedObjects;

    public bool usePositionalOffset = false;
    public bool overrideScale = false;

    public Vector3 positionalOffset;
    public Vector3 newScale;

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
        serialized.ApplyModifiedProperties();
        EditorGUILayout.LabelField("Selected Objects", selectedObjects.Length.ToString());

        if (GUILayout.Button("Swap Selected Objects w/ Prefab"))
        {
            Swap();
        }

        usePositionalOffset = EditorGUILayout.Toggle("Use Positional Offset?", usePositionalOffset);

        if(usePositionalOffset)
            positionalOffset = EditorGUILayout.Vector3Field("Positional Offset: ", positionalOffset);

        overrideScale = EditorGUILayout.Toggle("Override Scale?", overrideScale);

        if (overrideScale)
            newScale = EditorGUILayout.Vector3Field("New Scale: ", newScale);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selectedObjects = Selection.gameObjects;
    }

    void Swap()
    {
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            
            Vector3 savedPosition = selectedObjects[i].transform.position;
            Quaternion savedRotation = selectedObjects[i].transform.rotation;
            Vector3 savedScale = selectedObjects[i].transform.localScale;
            DestroyImmediate(selectedObjects[i]);
            GameObject newObj = GameObject.Instantiate(prefab);

            if (usePositionalOffset)
                savedPosition += positionalOffset;

            if (overrideScale)
                savedScale = newScale;

            newObj.transform.position = savedPosition;
            newObj.transform.rotation = savedRotation;
            newObj.transform.localScale = savedScale;
        }
        
    }
}
