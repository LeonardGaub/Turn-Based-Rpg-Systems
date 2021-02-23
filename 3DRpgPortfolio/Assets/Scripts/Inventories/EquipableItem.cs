using UnityEngine;

namespace Rpg.Inventories
{
    [CreateAssetMenu(menuName = ("Rpg.UI.InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        [SerializeField] private int healthBonus;
        [SerializeField] private int damageBonus;
        [SerializeField] private int speedBonus;

        public int HealthBonus => healthBonus;
        public int DamageBonus => damageBonus;
        public int SpeedBonus => speedBonus;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }

        public override string GetStats()
        {
            string retVal = "";
            if(healthBonus > 0)
            {
                retVal += "Health + " + healthBonus;
            }
            if(damageBonus > 0)
            {
                retVal += "Damage + " + damageBonus;
            }
            if(speedBonus > 0)
            {
                retVal += "Speed + " + speedBonus;
            }
            return retVal;
        }
    }
}