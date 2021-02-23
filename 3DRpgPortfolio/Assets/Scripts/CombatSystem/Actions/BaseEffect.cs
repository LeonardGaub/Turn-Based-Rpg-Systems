using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Effects
{
    public class BaseEffect
    {
        protected int duration;

        public BaseEffect(int duration)
        {
            this.duration = duration;
        }
        public virtual void Invoke(Actor user)
        {
            
        }

        public void Tick(Actor user)
        {
            duration--;
            if (duration <= 0)
            {
                user.RemoveEffect(this);
            }
        }
    }
}
