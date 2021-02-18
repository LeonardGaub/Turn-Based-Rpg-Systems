using Rpg.BattleSystem.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public abstract class ActorData : ScriptableObject
    {
        [SerializeField] List<BaseAction> abilities = new List<BaseAction>();
        public Vector3 offset = new Vector3(0, 0, 0);
        public List<BaseAction> Abilities => abilities;
        public string objectiveString;

    }
}
