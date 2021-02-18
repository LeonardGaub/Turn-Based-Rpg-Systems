using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

  
    public void OnInteract(PlayerDialogue playerDialogue)
    {
        if(dialogue == null) { return; };

        playerDialogue.StartDialogue(dialogue, this);
    }
}
