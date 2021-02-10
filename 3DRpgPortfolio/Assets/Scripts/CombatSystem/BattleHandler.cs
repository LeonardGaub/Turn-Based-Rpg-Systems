﻿using Rpg.BattleSystem.Actors;
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
        public BattleData data;


        List<Actor> characters = new List<Actor>();

        private int counter;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Actor.OnFinished += NextCharacter;

            switch (data.state)
            {
                case BattleData.TransitionState.InBattle:
                    break;
                case BattleData.TransitionState.OutBattle:
                    FindObjectOfType<SavingSystem>().Load("sav.data");
                    data.state = BattleData.TransitionState.InWorld;
                    break;
            }
        }

        public void SetUpBattle(List<PlayerActor> players, List<EnemyActor> enemies, Vector3 originalPosition)
        {
            data.ClearLists();
            data.state = BattleData.TransitionState.InBattle;
            data.originalPlayerPosition = originalPosition;
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
            characters = characters.OrderByDescending(characters => characters.speed).ToList();
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
                data.state = BattleData.TransitionState.OutBattle;
                onBattleOver.Invoke();
                SceneManager.LoadScene(0);
            }
            BuildCharacterList();
            print("Characters Left: " + characters.Count);
            counter = -1;
            NextCharacter();
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
    }
}