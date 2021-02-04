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
            abilities.Add(new BaseAction());
        }

        public override IEnumerator Turn()
        {
            if (!isAlive)
            {
                EndTurn();
                yield break;
            }
            Attack();
            yield return new WaitForSeconds(1f);
            EndTurn();
        }

        private void Attack()
        {
            int randomTarget = Random.Range(0, players.Count - 1);
            var randomAbility = Random.Range(0, abilities.Count - 1);
            abilities[randomAbility].Execute(this, players[randomTarget]);
        }
    }
}


