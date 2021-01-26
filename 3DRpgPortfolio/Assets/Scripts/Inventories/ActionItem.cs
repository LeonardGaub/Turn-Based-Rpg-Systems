using UnityEngine;

namespace Rpg.Inventories
{
    [CreateAssetMenu(menuName = ("Rpg.UI.InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        [SerializeField] bool consumable = false;

        public virtual void Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}