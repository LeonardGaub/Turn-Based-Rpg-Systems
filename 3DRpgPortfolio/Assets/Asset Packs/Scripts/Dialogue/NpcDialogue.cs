using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    

    public bool HandleRaycast(/*PlayerController callingController*/)
    {
        if (dialogue == null)
        {
            return false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //callingController.GetComponent<PlayerDialogue>().StartDialogue(dialogue, this);
        }
        return true;
    }
}
