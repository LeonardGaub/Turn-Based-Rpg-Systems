using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
    [SerializeField] Vector2 newNodeOffset = new Vector2(300, 0);
    
    Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

    
    private void OnValidate()
    {
        nodeLookup.Clear();        
        foreach (var node in GetNodes())
        {
            nodeLookup.Add(node.name, node);
        }
    }

    public IEnumerable<DialogueNode> GetNodes()
    {
        return nodes;
    }

    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }

    public IEnumerable<DialogueNode> GetAllPlayerChildren(DialogueNode currentNode)
    {
        foreach (var node in GetChildren(currentNode))
        {
            if (node.GetSpeaker() == Speaker.Player)
            {
                yield return node;
            }
        }
    }
    
    public IEnumerable<DialogueNode> GetAllNpcChildren(DialogueNode currentNode)
    {
        foreach (var node in GetChildren(currentNode))
        {
            if (node.GetSpeaker() != Speaker.Player)
            {
                yield return node;
            }
        }
    }

    public IEnumerable<DialogueNode> GetChildren(DialogueNode parentNode)
    {
        foreach (var id in parentNode.GetChildren())
        {
            if (nodeLookup.ContainsKey(id))
            {
                yield return nodeLookup[id];
            }
        }
    }
#if UNITY_EDITOR
    public void CreateNode(DialogueNode parentNode)
    {
        DialogueNode newNode = MakeNode(parentNode);
        Undo.RegisterCreatedObjectUndo(newNode, "Create New Node");
        Undo.RecordObject(this, "Creating Dialogue Node");
        AddNewNode(newNode);
    }
    
    public void DeleteNode(DialogueNode selectedNode)
    {
        Undo.RecordObject(this, "Deleting Dialogue Node");
        nodes.Remove(selectedNode);
        OnValidate();
        CleanUpChildren(selectedNode);
        Undo.DestroyObjectImmediate(selectedNode);
    }
    
    private DialogueNode MakeNode(DialogueNode parentNode)
    {
        DialogueNode newNode = CreateInstance<DialogueNode>();
        newNode.name = Guid.NewGuid().ToString();

        if (parentNode != null)
        {
            parentNode.AddChild(newNode.name);   
            newNode.SetSpeaker(parentNode.GetSpeaker() == Speaker.Player ? Speaker.Npc : Speaker.Player);
            newNode.SetRectPosition(parentNode.GetRect().position + newNodeOffset);
        }
        
        return newNode;
    }

    private void AddNewNode(DialogueNode newNode)
    {
        nodes.Add(newNode);
        AssetDatabase.AddObjectToAsset(newNode, this);
        OnValidate();
    }

    private void CleanUpChildren(DialogueNode selectedNode)
    {
        foreach (var node in GetNodes())
        {
            node.RemoveChild(selectedNode.name);
        }
    }
#endif
    
    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (nodes.Count == 0)
        {
            DialogueNode firstNode = MakeNode(null);
            AddNewNode(firstNode);
        }
        if (AssetDatabase.GetAssetPath(this) != "")
        {
            foreach (DialogueNode node in GetNodes())
            {
                if (!AssetDatabase.Contains(node))
                {
                    AssetDatabase.AddObjectToAsset(node, this);
                }
            }
        }
#endif
    }

    public void OnAfterDeserialize()
    {
    }
}
