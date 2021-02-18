using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    PlayerDialogue player = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerDialogue>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player != null)
        {
            transform.LookAt(player.transform);
            GetComponent<NpcDialogue>().OnInteract(player);
        }
    }
}
