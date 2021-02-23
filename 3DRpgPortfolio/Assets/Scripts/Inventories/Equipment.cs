using System;
using System.Collections.Generic;
using UnityEngine;
using Rpg.Saving;

namespace Rpg.Inventories
{
    public class Equipment : MonoBehaviour, ISaveable
    { 
        Dictionary<PlayerData, Dictionary<EquipLocation, EquipableItem>> equipedItems = new Dictionary<PlayerData, Dictionary<EquipLocation, EquipableItem>>();
        
        public event Action equipmentUpdated;
        public Action<EquipableItem> onRemoveItem;
        public Action onCharacterSwitch;
        public List<PlayerData> players = new List<PlayerData>();

        public PlayerData selectedPlayer;

        private void Start()
        {
            foreach(var player in players)
            {
                equipedItems.Add(player, new Dictionary<EquipLocation, EquipableItem>());
            }
        }

        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!equipedItems[selectedPlayer].ContainsKey(equipLocation))
            {
                return null;
            }

            return equipedItems[selectedPlayer][equipLocation];
        }

        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.GetAllowedEquipLocation() == slot);

            equipedItems[selectedPlayer][slot] = item;

            equipmentUpdated?.Invoke();
            
        }

        public void RemoveItem(EquipLocation slot)
        {
            onRemoveItem.Invoke(GetItemInSlot(slot));
            equipedItems[selectedPlayer].Remove(slot);
            equipmentUpdated?.Invoke();  
        }

        public Dictionary<EquipLocation, EquipableItem> GetEquipedItems()
        {
            return equipedItems[selectedPlayer];
        }

        public void ChangeSelectedPlayer(int index)
        {
            if(players[index] == null) { return; }
            selectedPlayer = players[index];
            onCharacterSwitch?.Invoke();
        }

        object ISaveable.CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<int, Dictionary<EquipLocation, string>>();
            for (int i = 0; i < players.Count; i++)
            {
                equippedItemsForSerialization.Add(i, new Dictionary<EquipLocation, string>());
                foreach(var item in equipedItems[players[i]])
                {
                    equippedItemsForSerialization[i].Add(item.Key, item.Value.GetItemID());
                }
            }
            return equippedItemsForSerialization;
        }

        void ISaveable.RestoreState(object state)
        {
            equipedItems = new Dictionary<PlayerData, Dictionary<EquipLocation, EquipableItem>>();

            var equippedItemsForSerialization = (Dictionary<int, Dictionary<EquipLocation, string>>)state;

            for (int i = 0; i < players.Count; i++)
            {
                equipedItems[players[i]] = new Dictionary<EquipLocation, EquipableItem>();
                foreach(var pair in equippedItemsForSerialization[i])   
                {
                    var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                    if(item != null)
                    {
                        equipedItems[players[i]][pair.Key] = item;
                    }
                }
            }
            equipmentUpdated?.Invoke();
        }
    }
}