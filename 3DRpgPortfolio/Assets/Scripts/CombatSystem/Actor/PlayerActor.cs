using Rpg.BattleSystem.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class PlayerActor : Actor
    {
        private List<EnemyActor> availableEnemies = new List<EnemyActor>();

        public override IEnumerator Turn()
        {
            availableEnemies = BattleHandler.GetAliveEnemies();
            if (availableEnemies.Count <= 0)
            {
                EndTurn();
                yield break;
            }
            yield return new WaitUntil(() => action != null && target != null);
            Attack(() => 
            {
                ClearAttack();
                EndTurn();  
            });
        }

        public void OnActionChoose(BaseAction action)
        {
            this.action = action;
        }

        public void OnTargetChoose(Actor target)
        {
            this.target = target;
        }
    }
}
