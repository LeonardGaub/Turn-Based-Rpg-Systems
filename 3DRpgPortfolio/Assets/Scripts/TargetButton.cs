using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.BattleSystem.UI
{
    public class TargetButton : MonoBehaviour
    {
        private EnemyActor enemy;

        public EnemyActor Enemy => enemy;

        public void SetUp(EnemyActor enemy)
        {
            this.enemy = enemy;
            GetComponentInChildren<Text>().text = enemy.Data.name;
        }
    }
}
