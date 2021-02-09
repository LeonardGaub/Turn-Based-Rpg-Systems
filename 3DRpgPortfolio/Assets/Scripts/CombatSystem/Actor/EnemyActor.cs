using Rpg.BattleSystem.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class EnemyActor : Actor
    {
        List<Actor> players;

        public void SetUp(List<Actor> players)
        {
            this.players = players;
        }

        public override IEnumerator Turn()
        {
            if (!isAlive)
            {
                EndTurn();
                yield break; 
            }
            yield return new WaitForSeconds(1f);
            Attack();
            EndTurn();
        }

        private void Attack()
        {
            print("Enemy attacks");
            int randomTarget = Random.Range(0, players.Count - 1);
            var randomAbility = Random.Range(0, data.Abilities.Count - 1);
            data.Abilities[randomAbility].Execute(this, players[randomTarget]);
        }
    }
}


