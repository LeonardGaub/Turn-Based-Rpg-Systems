using System;
using System.Runtime.InteropServices;
using Unity.MPE;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Graphs;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditor : EditorWindow
{
    private Dialogue selectedDialogue;
    private Vector2 scrollPosition;

    [NonSerialized]
    private DialogueNode nodeToDrag;
    [NonSerialized]
    private DialogueNode nodeToCreate;
    [NonSerialized] 
    private DialogueNode nodeToDelete;
    [NonSerialized] 
    private DialogueNode nodeToLink;

    private bool draggingCanvas;
    private Vector2 draggingOffset;
    
    const float CanvasSize = 4000;
    private const float BackgroundSize = 50;
    
    [MenuItem("Window/Dialogue Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "DialogueEditor");
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        var asset = EditorUtility.InstanceIDToObject(instanceId) as Dialogue;
        if(asset != null)
        {
            ShowEditorWindow();
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChange;
    }

    private void OnSelectionChange()
    {
        var selected = Selection.activeObject as Dialogue;
        if (selected != null)
        {
            selectedDialogue = selected;
            Repaint();
        }
    }

    private void OnGUI()
    {
        if (selectedDialogue)
        {
            ProcessEvents();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            Rect rect = GUILayoutUtility.GetRect(CanvasSize, CanvasSize);
            Texture2D texture2D = Resources.Load("background") as Texture2D;
            Rect texCoords = new Rect(0,0, CanvasSize / BackgroundSize, CanvasSize / BackgroundSize);
            
            GUI.DrawTextureWithTexCoords(rect, texture2D, texCoords);
            
            foreach (var node in selectedDialogue.GetNodes())
            {
                DrawConnections(node);
            }
            
            foreach (var node in selectedDialogue.GetNodes())
            {
                DrawNode(node);
            }
            EditorGUILayout.EndScrollView();
            
            if (nodeToCreate != null)
            {
                selectedDialogue.CreateNode(nodeToCreate);
                nodeToCreate = null;
            }

            if (nodeToDelete != null)
            {
                selectedDialogue.DeleteNode(nodeToDelete);
                nodeToDelete = null;
            }
        }
    }
    
    private void ProcessEvents()
    {
        if (Event.current.type == EventType.MouseDown && nodeToDrag == null)
        {
            nodeToDrag = GetNodeAtMousePosition(Event.current.mousePosition + scrollPosition);
            if (nodeToDrag != null)
            {
                draggingOffset = nodeToDrag.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = nodeToDrag;    
            }
            else
            {
                draggingCanvas = true;
                draggingOffset = Event.current.mousePosition + scrollPosition;
                Selection.activeObject = selectedDialogue;
            }
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
            if (nodeToDrag != null)
            {
                nodeToDrag.SetRectPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if(draggingCanvas)
            {
                scrollPosition = draggingOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
        }
        else if (Event.current.type == EventType.MouseUp && nodeToDrag != null)
        {
            nodeToDrag = null;
            draggingCanvas = false;
        }
    }

    private DialogueNode GetNodeAtMousePosition(Vector2 currentMousePosition)
    {
        DialogueNode returnNode = null;
        foreach (var node in selectedDialogue.GetNodes())
        {
            if (node.GetRect().Contains(currentMousePosition))
            {
                returnNode = node;
            }
        }
        return returnNode;
    }

    private void DrawNode(DialogueNode node)
    {
        GUIStyle style = node.GetNodeStyle();
        GUILayout.BeginArea(node.GetRect(), style);

        var newText = EditorGUILayout.TextField(node.GetText());
        node.SetText(newText);
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("+"))
        {
            nodeToCreate = node;
        }
        
        DrawLinkedButtons(node);
        
        if (GUILayout.Button("x"))
        {
            nodeToDelete = node;
        } 
        
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawLinkedButtons(DialogueNode node)
    {
        if (nodeToLink == null)
        {
            if (GUILayout.Button("link"))
            {
                nodeToLink = node;
            }
        }
        else if(nodeToLink == node)
        {
            if (GUILayout.Button("cancle"))
            {
                nodeToLink = null;
            }
        }
        else if (nodeToLink.GetChildren().Contains(node.name))
        {
            if (GUILayout.Button("unlink"))
            {
                nodeToLink.RemoveChild(node.name);
                nodeToLink = null;
            }
        }
        else
        {
            if (GUILayout.Button("child"))
            {
                nodeToLink.AddChild(node.name);
                nodeToLink = null;
            }
        }
    }
    
    private void DrawConnections(DialogueNode node)
    {
        Vector3 startPosition = new Vector3(node.GetRect().xMax, node.GetRect().center.y);
        foreach (var childNode in selectedDialogue.GetChildren(node))
        {
            Vector3 childPosition = new Vector3(childNode.GetRect().xMin, childNode.GetRect().center.y);
            Vector3 controlPointOffset = childPosition - startPosition;
            controlPointOffset.y = 0;
            Handles.DrawBezier(
                startPosition, childPosition, 
                startPosition + controlPointOffset, childPosition - controlPointOffset, 
                Color.blue, null, 5f);
        }
    }
}
 