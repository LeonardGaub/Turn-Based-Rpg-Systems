using Rpg.BattleSystem.Actors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.BattleSystem.UI
{
    public class BattleTargetUI : MonoBehaviour
    {
        [SerializeField] TargetButton targetButtonPrefab;
        [SerializeField] Transform targetContent;
        [SerializeField] BattleUI battleUI;
        public void SetUpTargetList(List<EnemyActor> enemies, PlayerActor player)
        {
            ClearTargetList();
            for (int i = 0; i < enemies.Count; i++)
            {
                TargetButton targetButton = Instantiate(targetButtonPrefab, targetContent.transform);
                targetButton.SetUp(BattleHandler.GetAliveEnemies()[i]);

                targetButton.GetComponent<Button>().onClick.AddListener(delegate { player.OnTargetChoose(targetButton.Enemy); });
                targetButton.GetComponent<Button>().onClick.AddListener(() => this.gameObject.SetActive(false));
                targetButton.GetComponent<Button>().onClick.AddListener(()=> battleUI.BattleAbilityUI.gameObject.SetActive(false));
            }
        }

        private void ClearTargetList()
        {
            foreach (Transform child in targetContent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
