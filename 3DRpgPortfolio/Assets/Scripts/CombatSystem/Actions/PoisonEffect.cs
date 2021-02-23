using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Effects
{
    public class PoisonEffect : BaseEffect
    {
        private int damage;

        public PoisonEffect(int dmg, int duration): base(duration)
        {
            damage = dmg;
        }
        public override void Invoke(Actor user)
        {
            user.RecieveDamage(damage);
        }
    }
}
