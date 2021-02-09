using Rpg.BattleSystem.Actors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.BattleSystem.UI
{
    public class BattleUI : MonoBehaviour
    {
        [Header("Ability List")]
        [SerializeField] BattleAbilityUI abilityUI;

        [Space]
        [Header("Target List")]
        [SerializeField] BattleTargetUI targetUI;

        public BattleTargetUI TargetUI => targetUI;
        public BattleAbilityUI BattleAbilityUI => abilityUI;
    }
}
