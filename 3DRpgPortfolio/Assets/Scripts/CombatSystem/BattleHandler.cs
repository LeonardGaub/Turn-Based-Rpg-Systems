using Rpg.BattleSystem.Actors;
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
        public BattleData data;
   

        List<Actor> characters = new List<Actor>();
       
        private int counter;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Actor.OnFinished += NextCharacter;
        }


        public void SetUpBattle(List<PlayerActor> players, List<EnemyActor> enemies)
        {
            data.ClearLists();
            foreach(PlayerActor player in players)
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
            SetUpCharacters();
            NextCharacter();
        }

        private void SetUpCharacters()
        {
            characters.Clear();
            foreach (PlayerActor player in data.spawnedPlayers)
            {
                player.SetUp(data.enemies.Cast<EnemyActor>().ToList(), this);
            }
            foreach (EnemyActor enemy in data.spawnedEnemies)
            {
                enemy.SetUp(data.players);
            }
            characters = data.spawnedPlayers.Union(data.spawnedEnemies).ToList();
            characters = characters.OrderByDescending(characters => characters.Data.Speed).ToList();   
        }

        private void NextCharacter()
        {
            counter++;
            if (characters.Count > counter)
            {
                characters[counter].StartTurn();
                if (characters[counter] is PlayerActor)
                {
                    onNextCharacter.Invoke(characters[counter] as PlayerActor);
                }
                return;
            }
            EndTurn();
        }

        public void EndTurn()
        {
            RemoveDeadCharacters();
            if (IsBattleOver())
            {
                print("Battle Over");
                SceneManager.LoadScene(0);
                return;
            }
            SetUpCharacters();
            print("Characters Left: " +characters.Count);
            counter = 0;
            characters[counter].StartTurn();
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
                if(character is EnemyActor)
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
            if(data.spawnedEnemies.Count <= 0)
            {
                return true;
            }
            return false;
        }


        //Actions Start Command 
        //Actions Im Finished<Wer>
        //List<Character> 
        //Abarbeite Warten auf Antwort

        //Zwei Gruppen 
        //Warten auf Action
        //Command Design Pattern
        //Invokation List welcher Char was macht 
        //Durcharbeiten 

        //
    }
}