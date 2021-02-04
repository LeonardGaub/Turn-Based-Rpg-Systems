using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class PlayerActor : Actor
    {
        BattleHandler battleHandler;
        List<EnemyActor> enemies;

        public void SetUp(List<EnemyActor> enemies, BattleHandler battleHandler)
        {
            this.enemies = enemies;
            this.battleHandler = battleHandler;
        }

        public override IEnumerator Turn()
        {
            var availableEnemies = enemies.Where(val => val.isAlive).ToList();
            if (availableEnemies.Count <= 0)
            {
                EndTurn();
                yield break;
            }
            print($"{name}'s Turn");
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForSeconds(0.2f);
            Attack(availableEnemies);
        }

        private void Attack(List<EnemyActor> targets)
        {
            int random = Random.Range(0, targets.Count);

            enemies = targets;
            enemies[random].RecieveDamage(Data.Damage);
            
            EndTurn();
        }
    }
}


