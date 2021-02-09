using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actions
{ 
    public class NormalAttack : BaseAction
    {
        public override void Execute(Actor user, Actor target)
        {
            print($"{user.Data.name} uses a normal Attack on {target.name}");
            target.RecieveDamage(user.Data.Damage);
        }
    }
}
