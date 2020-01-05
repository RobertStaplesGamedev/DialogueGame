using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

[CreateAssetMenu(menuName = "Dialogue System/Dialogue Node")]
public class DialogueNodeData : ScriptableObject
{
    public string speaking;
    public enum DialogueType { DialogueOptions, Dialogue }
    public DialogueType nodeType;
    [SerializeField] public List<DialogueData> options;

    [TextArea]
    public string phrase;
    public DialogueNodeData nextNode;

    // public DialogueNode CreateNode()
    // {
    //     DialogueNode thisNode;
    //     if (nodeType == DialogueType.DialogueOptions)
    //     {
    //         DialogueOptions newOptions = new DialogueOptions(speaking);
    //         foreach (DialogueData option in options)
    //         {
    //             if (option.nextNode != null)
    //             {
    //                 if (option. == null)
    //                 {
    //                     newOptions.AddOption(option.option, new Dialogue(speaking, option.phrase, option.nextNode.CreateNode()));
    //                 }
    //             }
    //             else
    //             {
    //                 newOptions.AddOption(option.option, new Dialogue(speaking, option.phrase));
    //             }
    //         }
    //         thisNode = newOptions;
    //         return thisNode;
    //     }
    //     else
    //     {
    //         if (nextNode != null)
    //         {
    //             if (nextNode.thisNode == null)
    //             {
    //                 thisNode = new Dialogue(speaking, phrase, nextNode.CreateNode());
    //             }
    //             else
    //             {
    //                 thisNode = new Dialogue(speaking, phrase, nextNode.thisNode);
    //             }
    //         }
    //         else
    //         {
    //             thisNode = new Dialogue(speaking, phrase);
    //         }
    //         return thisNode;
    //     }
    // }

    [System.Serializable]
    public struct DialogueData
    {
        public string option;
        public string phrase;
        public DialogueNodeData nextNode;

        public DialogueData(string _option, string _phrase, DialogueNodeData _nextNode)
        {
            option = _option;
            phrase = _phrase;
            nextNode = _nextNode;
        }
    }
}
