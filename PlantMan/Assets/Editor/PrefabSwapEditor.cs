using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PrefabSwapEditor : EditorWindow
{

    public GameObject prefab;

    public GameObject[] selectedObjects;

    public bool usePositionalOffset = false;
    public bool overrideScale = false;

    public Vector3 positionalOffset;
    public Vector3 newScale;

    private GameObject[] deletedObjects;
    private GameObject[] lastSwapObjects;

    [MenuItem("My Tools/Prefab Swap")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PrefabSwapEditor));
    }

    public void OnGUI()
    {
        SerializedObject serialized = new SerializedObject(this);
        SerializedProperty prefabsProperty = serialized.FindProperty("prefab");

        

        EditorGUILayout.LabelField("Prefab Swap", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(prefabsProperty, true);
        serialized.ApplyModifiedProperties();

        if(selectedObjects != null)
            EditorGUILayout.LabelField("Selected Objects", selectedObjects.Length.ToString());

        if (GUILayout.Button("Swap Selected Objects w/ Prefab"))
        {
            if(selectedObjects != null && selectedObjects.Length > 0 && prefab != null)
                Swap();
        }

        usePositionalOffset = EditorGUILayout.Toggle("Use Positional Offset?", usePositionalOffset);

        if(usePositionalOffset)
            positionalOffset = EditorGUILayout.Vector3Field("Positional Offset: ", positionalOffset);

        overrideScale = EditorGUILayout.Toggle("Override Scale?", overrideScale);

        if (overrideScale)
            newScale = EditorGUILayout.Vector3Field("New Scale: ", newScale);

        if(lastSwapObjects != null && lastSwapObjects.Length > 0)
        {
            if(GUILayout.Button("Undo Swap"))
            {
                UndoSwap();
            }
        }

        if(GUILayout.Button("Save/Cleanup"))
        {
            Cleanup();
        }

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

    private void OnDestroy()
    {
        Cleanup();
    }

    void Swap()
    {
        if(deletedObjects != null && deletedObjects.Length > 0)
        for(int i = 0; i < deletedObjects.Length; i++)
        {
            DestroyImmediate(deletedObjects[i]);
        }
        

        deletedObjects = selectedObjects;

        lastSwapObjects = new GameObject[selectedObjects.Length];

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            
            Vector3 savedPosition = selectedObjects[i].transform.position;
            Quaternion savedRotation = selectedObjects[i].transform.rotation;
            Vector3 savedScale = selectedObjects[i].transform.localScale;

            Transform parentTransform = selectedObjects[i].transform.parent;

            selectedObjects[i].SetActive(false);
            GameObject newObj = Instantiate(prefab);
            newObj.transform.parent = parentTransform;

            if (usePositionalOffset)
                savedPosition += positionalOffset;

            if (overrideScale)
                savedScale = newScale;

            newObj.transform.position = savedPosition;
            newObj.transform.rotation = savedRotation;
            newObj.transform.localScale = savedScale;

            lastSwapObjects[i] = newObj;
        }

        Selection.objects = lastSwapObjects;

        EditorSceneManager.MarkAllScenesDirty();

    }

    void UndoSwap()
    {
        if (lastSwapObjects != null && lastSwapObjects.Length > 0)
        for (int i = 0; i < lastSwapObjects.Length; i++)
        {
            DestroyImmediate(lastSwapObjects[i]);
        }

        if (deletedObjects != null && deletedObjects.Length > 0)
        for (int i = 0; i < deletedObjects.Length; i++)
        {
            deletedObjects[i].SetActive(true);
        }

        deletedObjects = new GameObject[0];
        lastSwapObjects = new GameObject[0];
        EditorSceneManager.MarkAllScenesDirty();
    }

    void Cleanup()
    {
        if (deletedObjects != null && deletedObjects.Length > 0)
        for (int i = 0; i < deletedObjects.Length; i++)
        {
            DestroyImmediate(deletedObjects[i]);
        }
    }
}
