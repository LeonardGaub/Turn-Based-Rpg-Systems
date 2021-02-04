using Rpg.BattleSystem.Actors;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class BaseAction : MonoBehaviour
    {
        public virtual void Execute(Actor user, Actor target)
        {
            print($"{user.Data.name} uses a normal Attack on {target.name}");
        }
    }
}   