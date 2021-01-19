using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Speaker
{
   Player,
   Npc
}
public class DialogueNode: ScriptableObject
{
   [SerializeField] private string text;
   [SerializeField] private List<string> children = new List<string>();
   [SerializeField] private Rect rect = new Rect(0, 0, 200, 100);
   [SerializeField] private Speaker speaker;
   [SerializeField] private string[] onEnterAction;
   [SerializeField] private string[] onExitAction;
   [SerializeField] private Condition condition;

   public Rect GetRect()
   {
      return rect;
   }

   public string GetText()
   {
      return text;
   }

   public List<string> GetChildren()
   {
      return children;
   }

   public Speaker GetSpeaker()
   {
      return speaker;
   }

   public string[] GetEnterActions()
   {
      return onEnterAction;
   }
   
   public string[] GetExitActions()
   {
      return onExitAction;
   }

   public GUIStyle GetNodeStyle()
   {
      GUIStyle nodeStyle = new GUIStyle();
      nodeStyle.padding = new RectOffset(10,10,10,10);
      nodeStyle.border = new RectOffset(10,10,10,10);

      switch (speaker)
      {
         case Speaker.Player:
            nodeStyle.normal.background = Texture2D.normalTexture;
            return nodeStyle;
         case Speaker.Npc:
            nodeStyle.normal.background = Texture2D.whiteTexture;
            return nodeStyle;
      }
      
      nodeStyle.normal.background = Texture2D.redTexture;
      return nodeStyle;
   }

   public bool CheckCondtion(IEnumerable<IPredicateEvaluator> getEvaluators)
   {
      return condition.Check(getEvaluators);
   }
   
   #if UNITY_EDITOR
   public void SetRectPosition(Vector2 pos)
   {
      Undo.RecordObject(this, "Dragging Dialogue Node");
      rect.position = pos;
      EditorUtility.SetDirty(this);
   }

   public void SetSpeaker(Speaker newSpeaker)
   {
      Undo.RecordObject(this, "Set Speaker");
      speaker = newSpeaker;
      EditorUtility.SetDirty(this);
   }

   public void SetText(string newText)
   {
      if (newText != text)
      {
         Undo.RecordObject(this, "Setting new Text");
         text = newText;  
         EditorUtility.SetDirty(this);
      }
   }

   public void AddChild(string newChild)
   {
      Undo.RecordObject(this, "Add Child");
      children.Add(newChild);
      EditorUtility.SetDirty(this);
   }

   public void RemoveChild(string child)
   {
      Undo.RecordObject(this, "Remove Child");
      children.Remove(child);
      EditorUtility.SetDirty(this);
   }
   #endif
}
