using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            GetComponentInChildren<TextMeshProUGUI>().text = enemy.name;
        }

        public void OnPointerEnter()
        {
            if(enemy != null)
            {
                enemy.OnTargeted();
            }
        }

        public void OnPointerExit()
        {
            if(enemy != null)
            {
                enemy.OnDeTargeted();
            }
        }
    }
}
