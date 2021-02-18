using Rpg.BattleSystem.Actors;
using Rpg.BattleSystem.UI;
using Rpg.Saving;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class BattleScene : MonoBehaviour
    {
        [SerializeField] List<Transform> playerPosition = new List<Transform>();
        [SerializeField] List<Transform> enemyPositions = new List<Transform>();
        [SerializeField] BattleUI battleUI;
        [SerializeField] BattleData data;
        void Start()
        {
            SetUpBattleScene(data.players, data.enemies);
            FindObjectOfType<SavingSystem>().Load("battle.sav");
            BattleHandler.onNextCharacter += SetUpAbilities;
            BattleHandler.onBattleOver += RemoveListeners;

            BattleHandler.Instance.StartBattle();
        }

        private void SetUpBattleScene(List<Actor> players, List<Actor> enemies)
        {
            for (int i = 0; i < players.Count; i++)
            {
                var player = Instantiate(players[i], playerPosition[i].transform.position, playerPosition[i].rotation);
                data.spawnedPlayers.Add(player);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                var enemy = Instantiate(enemies[i], enemyPositions[i].transform.position, enemyPositions[i].rotation);
                data.spawnedEnemies.Add(enemy);
            }
        }

        private void SetUpAbilities(PlayerActor player)
        {
            battleUI.BattleAbilityUI.gameObject.SetActive(true);
            battleUI.BattleAbilityUI.SetUpPlayerAbilities(player);
        }

        private void RemoveListeners()
        {
            BattleHandler.onNextCharacter -= SetUpAbilities;
            BattleHandler.onBattleOver -= RemoveListeners;
        }
    }

}
