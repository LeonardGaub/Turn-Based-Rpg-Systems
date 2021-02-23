using Rpg.BattleSystem.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class EnemyActor : Actor
    {
        public override IEnumerator Turn()
        {
            if (!isAlive)
            {
                EndTurn();
                yield break; 
            }
            yield return new WaitForSeconds(1f);
            ChooseAttack();
            Attack(() =>
            {
                ClearAttack();
                EndTurn();
            });
        }

        public override void CopyData()
        {
            var copy = Instantiate(data);
            data = copy;
        }

        private void ChooseAttack()
        {
            var targets = BattleHandler.GetAlivePlayers();
            target = targets[UnityEngine.Random.Range(0, targets.Count)];

            var abilities = data.Abilities;
            action = abilities[UnityEngine.Random.Range(0, abilities.Count)];
        }
    }
}


