using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CanInteract();
        }
    }

    public bool CanInteract(/*PlayerController callingController*/)
    {
        if (dialogue == null)
        {
            return false;
        }
       // if (Input.GetMouseButtonDown(0))
       // {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogue>().StartDialogue(dialogue, this);
       // }
        return true;
    }
}
