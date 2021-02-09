using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class EnemyEncounter : MonoBehaviour
    {
        [SerializeField] List<EnemyActor> enemies = new List<EnemyActor>();

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<PlayerGroup>() != null)
            {
                var players = other.gameObject.GetComponent<PlayerGroup>().Players;
                BattleHandler.Instance.SetUpBattle(players, enemies);
            }
        }
    }
}
