using Rpg.Inventories;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rpg.UI.Inventories
{
    public class PlayerChange : MonoBehaviour
    {
        Equipment playerEquipment;
        [SerializeField] TextMeshProUGUI title; 

        private void Start()
        {
            playerEquipment = FindObjectOfType<Equipment>();
        }
        public void ChangeSelectedPlayer(int playerIndex)
        {
            playerEquipment.ChangeSelectedPlayer(playerIndex);
            title.text = playerEquipment.selectedPlayer.name;
        }
    }
}
