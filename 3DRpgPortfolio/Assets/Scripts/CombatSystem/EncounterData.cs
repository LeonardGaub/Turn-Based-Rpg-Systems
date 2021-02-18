using Rpg.BattleSystem.Actors;
using Rpg.BattleSystem.Reward;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem
{
    [CreateAssetMenu(menuName = ("RPG/Combat/EncounterData"))]
    public class EncounterData : ScriptableObject
    {
        [SerializeField] List<EnemyActor> enemies = new List<EnemyActor>();
        [SerializeField] RewardData reward;
        public List<EnemyActor> Enemies => enemies;
        public RewardData Reward => reward;
    }
}
