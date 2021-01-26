using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.Inventories
{
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        // CONFIG DATA
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string itemID = null;
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] string displayName = null;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField] [TextArea] string description = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] Sprite icon = null;
        [Tooltip("The prefab that should be spawned when this item is dropped.")]
        [SerializeField] Pickup pickup = null;
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] bool stackable = false;

        static Dictionary<string, InventoryItem> itemLookup;

        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookup == null)
            {
                itemLookup = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (itemLookup.ContainsKey(item.itemID))
                    {
                        continue;
                    }

                    itemLookup[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookup.ContainsKey(itemID)) return null;
            return itemLookup[itemID];
        }

        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickup);
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public bool IsStackable()
        {
            return stackable;
        }

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize(){}
    }
}
