using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.BattleSystem.UI
{
    public class BattleAbilityUI : MonoBehaviour
    {
        [SerializeField] AbilityButton abilityButtonPrefab;
        [SerializeField] Transform abilityContent;
        [SerializeField] private BattleUI battleUI;

        public void SetUpPlayerAbilities(PlayerActor player)
        {
            ClearAbilityList();
            for (int i = 0; i < player.Data.Abilities.Count; i++)
            {
                AbilityButton abilityButton = Instantiate(abilityButtonPrefab, abilityContent.transform);
                abilityButton.SetUp(player.Data.Abilities[i]);

                abilityButton.GetComponent<Button>().onClick.AddListener(delegate { player.OnActionChoose(abilityButton.Action); });
                abilityButton.GetComponent<Button>().onClick.AddListener(() => ChooseTarget(BattleHandler.GetAliveEnemies(), player));
            }
        }

        public void ChooseTarget(List<EnemyActor> enemies, PlayerActor player)
        {
            battleUI.TargetUI.gameObject.SetActive(true);
            battleUI.TargetUI.SetUpTargetList(enemies, player);
        }

        private void ClearAbilityList()
        {
            foreach (Transform child in abilityContent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
