using Rpg.BattleSystem.Actors;
using UnityEngine;

namespace Rpg.BattleSystem.Actions
{
    public enum AttackStartPoint
    {
        OnEnemy,
        OnSpot
    }
    public class BaseAction : MonoBehaviour
    {
        public AttackStartPoint attackStartPoint;
        public string animTrigger;
        public virtual void Execute(Actor user, Actor target)
        {
           
        }
    }
}   