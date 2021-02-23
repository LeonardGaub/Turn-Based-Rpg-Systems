﻿using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actions
{
    public class NormalSpell : BaseAction
    {
        public override void Execute(Actor user, Actor target)
        { 
            target.RecieveDamage(user.Data.damage * 2);
        }
    }
}
