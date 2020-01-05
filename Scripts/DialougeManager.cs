using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueSystem;

public class DialougeManager : MonoBehaviour
{
    public GameObject Conversation;
    public Conversation convo;
    private bool dialougeStarted = false;

    public TMP_Text NpcName;
    public TMP_Text Resposne;

    public Button Continue;
    public Button Question1;
    public Button Question2;
    public Button Question3;
    public Button Question4;

    public Button[] Questions;

    public DialogueNodeData startingNode;
    Dictionary<DialogueNodeData, DialogueNode> instantiatedNodes;

    void Start()
    {
        Button[] newQuestions = { Question1, Question2, Question3, Question4 };
        Questions = newQuestions;

        instantiatedNodes = new Dictionary<DialogueNodeData, DialogueNode>();

        //Start the conversation
        convo = new Conversation(new string[] { "derek", "player" }, CreateDialogue(startingNode));
        ShowNpcResponse(convo.ContinueConversaton());
    }

    DialogueNode CreateDialogue(DialogueNodeData _data)
    {
        DialogueNode newData;
        if (_data.nodeType == DialogueNodeData.DialogueType.DialogueOptions)
        {
            DialogueOptions newOptions = new DialogueOptions(_data.speaking);
            newData = newOptions;
            instantiatedNodes.Add(_data, newData);
            List<string> doneOptions = new List<string>();
            foreach (DialogueNodeData.DialogueData option in _data.options)
            {
                if (option.nextNode != null)
                {
                    if (instantiatedNodes.ContainsKey(option.nextNode))
                    {
                        // doneOptions.Add(option.option);
                        newOptions.AddOption(option.option, new Dialogue(_data.speaking, option.phrase, instantiatedNodes[option.nextNode]));
                    }
                    else if (!doneOptions.Contains(option.option))
                    {
                        doneOptions.Add(option.option);
                        newOptions.AddOption(option.option, new Dialogue(_data.speaking, option.phrase, CreateDialogue(option.nextNode)));
                    }
                }
                else
                {
                    newOptions.AddOption(option.option, new Dialogue(_data.speaking, option.phrase));
                }
            }
        }
        else
        {
            newData = new Dialogue(_data.speaking, _data.phrase);
            if (_data.nextNode != null)
            {
                if (instantiatedNodes.ContainsKey(_data.nextNode))
                {
                    newData = new Dialogue(_data.speaking, _data.phrase, instantiatedNodes[_data.nextNode]);
                    instantiatedNodes.Add(_data, newData);
                }
                else
                {
                    instantiatedNodes.Add(_data, newData);
                    newData = new Dialogue(_data.speaking, _data.phrase, CreateDialogue(_data.nextNode));
                }
            }
            else
            {
                newData = new Dialogue(_data.speaking, _data.phrase);
            }

        }
        return newData;

    }

    void StartDialouge(Dialogue dialogue)
    {
        NpcName.text = dialogue.speaking;
    }

    public void ClickContinue()
    {
        if (convo.CurrentDialouge.GetType() == (typeof(DialogueOptions)))
        {
            DialogueOptions thisDialouge = convo.CurrentDialouge as DialogueOptions;
            ShowQuestions(thisDialouge.Options);
        }
        else
        {
            ShowNpcResponse(convo.ContinueConversaton());
        }

    }

    public void ClickOption(Button button)
    {
        DialogueOptions thisDialouge = convo.CurrentDialouge as DialogueOptions;
        Dialogue newResponse = thisDialouge.Options[button.GetComponentInChildren<TMP_Text>().text];
        convo.ChooseOption(newResponse);
        ShowNpcResponse(newResponse.phrase);
    }

    void ShowNpcResponse(string newResponse)
    {
        Resposne.gameObject.SetActive(true);
        Continue.gameObject.SetActive(true);

        Question1.gameObject.SetActive(false);
        Question2.gameObject.SetActive(false);
        Question3.gameObject.SetActive(false);
        Question4.gameObject.SetActive(false);
        Resposne.text = newResponse;
    }

    void ShowQuestions(Dictionary<string, Dialogue> options)
    {
        Resposne.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);

        //TODO: Need a better way to enumaterate
        int i = 0;
        foreach (KeyValuePair<string, Dialogue> item in options)
        {
            Questions[i].gameObject.SetActive(true);
            Questions[i].transform.GetComponentInChildren<TMP_Text>().text = item.Key;
            i++;
        }
    }
}
