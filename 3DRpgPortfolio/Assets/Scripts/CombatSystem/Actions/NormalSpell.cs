using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actions
{
    public class NormalSpell : BaseAction
    {
        public override void Execute(Actor user, Actor target)
        {
            print($"{user.Data.name} uses a magic Attack on {target.name}");
            target.RecieveDamage(user.Data.Damage * 2);
        }
    }
}
