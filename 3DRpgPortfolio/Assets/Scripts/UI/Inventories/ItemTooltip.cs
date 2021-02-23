using UnityEngine;
using TMPro;
using Rpg.Inventories;

namespace Rpg.UI.Inventories
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;
        [SerializeField] TextMeshProUGUI statsText = null;

        public void Setup(InventoryItem item)
        {
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();
            statsText.text = item.GetStats();
        }
    }
}
