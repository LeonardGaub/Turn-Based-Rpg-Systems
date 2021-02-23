using Rpg.BattleSystem.Actors;
using Rpg.BattleSystem.Reward;
using Rpg.Inventories;
using Rpg.Saving;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rpg.BattleSystem
{
    public class BattleHandler : MonoBehaviour
    {
        #region Singleton
        private static BattleHandler instance;
        public static BattleHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<BattleHandler>();
                };
                return instance;
            }
        }
        #endregion

        public static Action<PlayerActor> onNextCharacter;
        public static Action onBattleOver;
        public static Action<bool,RewardData> onUIShow;
        public BattleData data;


        List<Actor> characters = new List<Actor>();

        private int counter;

        private void Start()
        {
            DontDestroyOnLoad(this);
            Actor.OnFinished += NextCharacter;

            switch (BattleData.state)
            {
                case BattleData.TransitionState.OutBattleWon:
                    FindObjectOfType<SavingSystem>().Load("sav.data");
                    BattleData.state = BattleData.TransitionState.InWorld;
                    GiveRewards(data.reward);
                    UpdateQuestList();
                    break;
                case BattleData.TransitionState.OutBattleLost:
                    FindObjectOfType<SavingSystem>().Load("sav.data");
                    BattleData.state = BattleData.TransitionState.InWorld;
                    break;

            }
        }

        public void SetUpBattle(List<PlayerActor> players, List<EnemyActor> enemies, Vector3 originalPosition, RewardData reward)
        {
            data.ClearLists();
            BattleData.state = BattleData.TransitionState.InBattle;
            data.originalPlayerPosition = originalPosition;
            data.reward = reward;
            foreach (PlayerActor player in players)
            {
                data.players.Add(player);
            }

            foreach (EnemyActor enemy in enemies)
            {
                data.enemies.Add(enemy);
            }
            SceneManager.LoadScene(1);
        }

        public void StartBattle()
        {
            counter = -1;
            SetStats();
            SetUpCharacters();
            NextCharacter();
        }


        private void SetUpCharacters()
        {
            BuildCharacterList();
        }

        private void BuildCharacterList()
        {
            characters.Clear();
            characters = data.spawnedPlayers.Union(data.spawnedEnemies).ToList();
            characters = characters.OrderByDescending(characters => characters.Data.speed).ToList();
        }

        private void SetStats()
        {
            foreach (PlayerActor player in data.spawnedPlayers)
            {
                player.SetUp();
            }
            foreach (EnemyActor enemy in data.spawnedEnemies)
            {
                enemy.SetUp();
            }
        }
        private void NextCharacter()
        {
            BuildCharacterList();
            counter++;
            if (characters.Count > counter)
            {
                if (!characters[counter].isAlive) { NextCharacter(); return; }
                if (characters[counter] is PlayerActor)
                {
                    onNextCharacter.Invoke(characters[counter] as PlayerActor);
                }
                characters[counter].StartTurn();
                return;
            }
            EndTurn();
        }

        public static List<EnemyActor> GetAliveEnemies()
        {
            List<EnemyActor> availableEnemies = new List<EnemyActor>();
            foreach (EnemyActor enemy in instance.data.spawnedEnemies)
            {
                if (enemy.isAlive)
                {
                    availableEnemies.Add(enemy);
                }
            }
            return availableEnemies;
        }

        public static List<PlayerActor> GetAlivePlayers()
        {
            List<PlayerActor> availablePlayers = new List<PlayerActor>();
            foreach (PlayerActor player in instance.data.spawnedPlayers)
            {
                if (player.isAlive)
                {
                    availablePlayers.Add(player);
                }
            }
            return availablePlayers;
        }

        public void EndTurn()
        {
            RemoveDeadCharacters();
            if (IsBattleOver())
            {
                BattleOver(data.spawnedPlayers.Count > 0);
            }
            BuildCharacterList();
            counter = -1;
            NextCharacter();
        }

        private void BattleOver(bool playersWon)
        {
            onBattleOver?.Invoke();
            onUIShow.Invoke(playersWon, data.reward);
        }

        private void RemoveDeadCharacters()
        {
            var deadCharacters = characters.Where(val => !val.isAlive).ToList();
            foreach (Actor character in deadCharacters)
            {
                if (character is PlayerActor)
                {
                    data.spawnedPlayers.Remove(character);
                    Destroy(character.gameObject);
                }
                if (character is EnemyActor)
                {
                    data.spawnedEnemies.Remove(character);
                    Destroy(character.gameObject);
                }
            }
        }

        private bool IsBattleOver()
        {
            if (data.spawnedPlayers.Count <= 0)
            {
                return true;
            }
            if (data.spawnedEnemies.Count <= 0)
            {
                return true;
            }
            return false;
        }

        private void GiveRewards(RewardData reward)
        {
            foreach(var item in reward.Items)
            {
                FindObjectOfType<Inventory>().AddToFirstEmptySlot(item, 1);
            }
            print(reward.Xp);
        }

        private void UpdateQuestList()
        {
            foreach(EnemyActor enemy in data.enemies)
            {
                FindObjectOfType<QuestList>().KilledEnemy(enemy.Data.objectiveString);
            }
        }
    }
}