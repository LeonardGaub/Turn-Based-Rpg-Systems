using Rpg.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Reward
{
    [CreateAssetMenu(menuName = ("RPG/Combat/RewardData"))]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> itemRewards = new List<InventoryItem>();
        [SerializeField] private int xpGain = 0;

        public List<InventoryItem> Items => itemRewards;
        public int Xp => xpGain;
    }
}
