using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[ExecuteAlways]
public class CustomGUID : MonoBehaviour
{
    public string guid;

#if UNITY_EDITOR
    private void Awake()
    {
        if (!Application.IsPlaying(gameObject))
        {
            SerializedObject serializedObject = new SerializedObject(this);

            SerializedProperty guidProperty = serializedObject.FindProperty(nameof(guid));

            // NOTE(Chris): This may have performance problems, since it's basically O(n^2) where n = the number of
            // CustomGUIDs in the scene. If there is a problem, I can probably optimize it.
            CustomGUID[] customGuids = FindObjectsOfType<CustomGUID>();
            foreach (CustomGUID customGuid in customGuids)
            {
                if (customGuid.guid == guid && customGuid != this)
                {
                    guidProperty.stringValue = NewGuid();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            SerializedObject serializedObject = new SerializedObject(this);

            SerializedProperty guidProperty = serializedObject.FindProperty(nameof(guid));

            if (IsPrefab())
            {
                guidProperty.stringValue = null;
            }
            else if (String.IsNullOrEmpty(guidProperty.stringValue) ||
                     (guidProperty.isInstantiatedPrefab &&!guidProperty.prefabOverride))
            {
                guidProperty.stringValue = NewGuid();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

#if UNITY_EDITOR
    private bool IsPrefab()
    {
        PrefabStage currPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

        return currPrefabStage != null &&
               currPrefabStage.IsPartOfPrefabContents(gameObject);
    }
#endif

    private string NewGuid()
    {
        return Guid.NewGuid().ToString();
    }
}