using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PlayerDialogue : MonoBehaviour
{
    private Dialogue _currentDialogue;
    private DialogueNode _currentNode;
    private bool _isChoosing;
    private NpcDialogue _currentNpc;

    public event Action OnDialogueUpdated;
   
    public bool HasDialogue()
    {
        return _currentDialogue != null;
    }

    public void StartDialogue(Dialogue newDialogue, NpcDialogue newNpc)
    {
        _currentNpc = newNpc;
        _currentDialogue = newDialogue;
        _currentNode = _currentDialogue.GetRootNode();
        TriggerEnterActions();
        OnDialogueUpdated.Invoke();
    }

    public bool IsChoosing()
    {
        return _isChoosing;
    }

    public string GetText()
    {
        return _currentNode != null ? _currentNode.GetText() : " ";
    }

    public IEnumerable<DialogueNode> GetChoices()
    {
        return FilterOnCondition(_currentDialogue.GetAllPlayerChildren(_currentNode));
    }

    public void SelectChoice(DialogueNode chosenNode)
    {
        _currentNode = chosenNode;
        TriggerEnterActions();
        _isChoosing = false;
        //Show Answer in textBox again
        Next();
    }
    
    public void Next()
    {
        int responses = FilterOnCondition(_currentDialogue.GetAllPlayerChildren(_currentNode)).Count();
        if (responses > 0)
        {
            _isChoosing = true;
            TriggerExitActions();
            OnDialogueUpdated.Invoke();
            return;
        }
        
        DialogueNode[] children = FilterOnCondition(_currentDialogue.GetAllNpcChildren(_currentNode)).ToArray();
        TriggerExitActions();
        _currentNode = children.Length > 0 ? children[0] : null;
        TriggerEnterActions();
        if (_currentNode == null)
        {
            Quit();
        }
        
        OnDialogueUpdated.Invoke();
    }

    private void TriggerEnterActions()
    {
        if (_currentNode != null && _currentNode.GetEnterActions().Length > 0)
        {
            foreach (var action in _currentNode.GetEnterActions())
            {
                TriggerAction(action);
            }
        }
    }

    private void TriggerExitActions()
    {
        if (_currentNode != null && _currentNode.GetExitActions().Length > 0)
        {
            foreach (var action in _currentNode.GetExitActions())
            {
                TriggerAction(action);
            }
        }
    }

    private void TriggerAction(string action)
    {
        foreach (DialogueTrigger trigger in _currentNpc.GetComponents<DialogueTrigger>())
        {
            trigger.Trigger(action);
        }
    }

    public bool HasNext()
    {
        if (_currentNode == null)
        {
            return false;
        }
        return FilterOnCondition(_currentDialogue.GetChildren(_currentNode).ToArray()).Any();
    }

    private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
    {
        foreach (var node in inputNode)
        {
            if (node.CheckCondtion(GetEvaluators()))
            {
                yield return node;
            }
        }
    }

    private IEnumerable<IPredicateEvaluator> GetEvaluators()
    {
        return GetComponents<IPredicateEvaluator>();
    }

    public void Quit()
    {
        _currentDialogue = null;
        TriggerExitActions();
        _currentNpc = null;
        _currentNode = null;
        _isChoosing = false;
        OnDialogueUpdated();
    }
}
