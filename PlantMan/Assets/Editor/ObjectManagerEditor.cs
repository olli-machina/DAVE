using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectManagerEditor : EditorWindow
{
    public GameObject folder, prefab;
    public GameObject[] manualObjects;

    //how many objects are selected
    int numSelected;
    //the name of the folder they want to send selected objects to
    string folderNameString = "";
    //the name of the folder they want to send prefabs to
    string prefabFolderNameString = "";
    //the name of the objects
    string objectNameString = "";
    //the starting index number for the selected objects
    int startIndex = 0;
    //the starting index number for the prefabs
    int prefabStartIndex = 0;

    [MenuItem("My Tools/Object Manager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ObjectManagerEditor));
    }

    public void OnGUI()
    {
        // Array help found here https://stackoverflow.com/questions/47753367/how-to-display-modify-array-in-the-editor-window
        EditorGUILayout.LabelField("Prefab Placement", EditorStyles.boldLabel);
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty prefabsProperty = so.FindProperty("prefab");
        EditorGUILayout.PropertyField(prefabsProperty, true);
        so.ApplyModifiedProperties();
        prefabFolderNameString = EditorGUILayout.TextField("Folder Name", prefabFolderNameString);
        prefabStartIndex = EditorGUILayout.IntField("Starting Index", prefabStartIndex);
        if (GUILayout.Button("Move Prefabs"))
        {
            MovePrefabs();
        }

        EditorGUILayout.LabelField("Manual Placement", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Number Selected", manualObjects.Length.ToString());
        objectNameString = EditorGUILayout.TextField("Object Name", objectNameString);
        folderNameString = EditorGUILayout.TextField("Folder Name", folderNameString);
        startIndex = EditorGUILayout.IntField("Starting Index", startIndex);
        if (GUILayout.Button("Move and Rename Selected"))
        {
            MoveSelected();
        }
    }
    void Update()
    {
        manualObjects = Selection.gameObjects;
        if (manualObjects.Length != numSelected)
        {
            numSelected = manualObjects.Length;
            Repaint();
        }
    }

    void MoveSelected()
    {
        if (!GameObject.Find(folderNameString))
        {
            folder = new GameObject(folderNameString);
        }

        for (int i = 0; i < manualObjects.Length; i++)
        {
            int index = startIndex + i;
            if (objectNameString != "")
            {
                manualObjects[i].name = objectNameString + "_" + index;
            }
            manualObjects[i].transform.parent = folder.transform;
        }
    }

    void MovePrefabs()
    {
        GameObject[] temp;
        temp = FindObjectsOfType(typeof (GameObject)) as GameObject[];
        int index = 0;

        if (!GameObject.Find(prefabFolderNameString))
        {
            folder = new GameObject(prefabFolderNameString);
        }

        for (int i = 0; i < temp.Length; i++)
        {
            if(temp[i].gameObject.name.Contains(prefab.gameObject.name))
            {
                temp[i].name = prefab.name + "_" + index;
                index++;
                temp[i].transform.parent = folder.transform;
            }
            else
            {
                Debug.Log(temp[i].gameObject.name);
            }
        }
    }

    void FindObjects()
    {

    }

}

