using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem
{
    [CreateAssetMenu(menuName = ("RPG/Combat/EncounterData"))]
    public class EncounterData : ScriptableObject
    {
        [SerializeField] List<EnemyActor> enemies = new List<EnemyActor>();

        public List<EnemyActor> Enemies => enemies;
    }
}
