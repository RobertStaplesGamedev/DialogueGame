using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DialogueSystem;

[CustomEditor(typeof(DialogueNodeData))]
public class DialogueNodeDataEditor : Editor
{
    string option;
    string phrase;
    DialogueNodeData nextNode;

    bool removeAtIndex = false;
    int removeAtIndexInteger;

    public override void OnInspectorGUI()
    {
        DialogueNodeData data = (DialogueNodeData)target;
        if (data.options == null)
        {
            data.options = new List<DialogueNodeData.DialogueData>();
            Debug.Log("empty list");
        }
        if (data.nodeType == DialogueNodeData.DialogueType.Dialogue)
        {
            DrawDefaultInspector();
        }
        else if (data.nodeType == DialogueNodeData.DialogueType.DialogueOptions)
        {
            data.speaking = EditorGUILayout.TextField("Speaking", data.speaking);
            data.nodeType = (DialogueNodeData.DialogueType)EditorGUILayout.EnumPopup("Node Type", data.nodeType);
            for (int i = 0; i < data.options.Count; i++)
            {
                GUILayout.BeginHorizontal();
                DialogueNodeData.DialogueData newData;
                newData.option = EditorGUILayout.TextField(data.options[i].option);
                newData.phrase = EditorGUILayout.TextField(data.options[i].phrase);
                newData.nextNode = (DialogueNodeData)EditorGUILayout.ObjectField(data.options[i].nextNode, typeof(DialogueNodeData), false);
                if (GUILayout.Button("X"))
                {
                    removeAtIndex = true;
                    removeAtIndexInteger = i;
                }
                data.options[i] = newData;
                GUILayout.EndHorizontal();
            }
            if (removeAtIndex)
            {
                removeAtIndex = false;
                data.options.RemoveAt(removeAtIndexInteger);
            }

            GUILayout.BeginHorizontal();
            option = EditorGUILayout.TextField(option);
            phrase = EditorGUILayout.TextField(phrase);
            nextNode = (DialogueNodeData)EditorGUILayout.ObjectField(nextNode, typeof(DialogueNodeData), false);
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Add Option"))
            {
                data.options.Add(new DialogueNodeData.DialogueData(option, phrase, nextNode));
                option = "new Option";
                phrase = "new Phrase";
            }
        }
    }
}
