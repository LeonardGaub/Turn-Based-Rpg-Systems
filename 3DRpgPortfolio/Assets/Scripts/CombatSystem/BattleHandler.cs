using Rpg.BattleSystem.Actors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class BattleHandler : MonoBehaviour
    {
        List<Actor> players = new List<Actor>();
        List<Actor> enemies = new List<Actor>();
        private int counter;

        List<Actor> characters = new List<Actor>();

        private void Start()
        {
            print("Start");
            StartBattle();
        }

        public void StartBattle()
        {
            Actor.OnFinished += NextCharacter;
            foreach (PlayerActor playerActor in GetComponents<PlayerActor>())
            {
                players.Add(playerActor);
            }
            foreach (EnemyActor enemyActor in GetComponents<EnemyActor>())
            {
                enemies.Add(enemyActor);
            }
            counter = 0;
            SetUpCharacters();
            characters[counter]?.StartTurn();
        }

        private void SetUpCharacters()
        {
            characters.Clear();
            foreach (PlayerActor player in players)
            {
                player.SetUp(enemies.Cast<EnemyActor>().ToList(), this);
            }
            foreach (EnemyActor enemy in enemies)
            {
                enemy.SetUp(players);
            }
            characters = players.Union(enemies).ToList();
            characters = characters.OrderByDescending(characters => characters.Data.Speed).ToList();   
        }

        private void NextCharacter()
        {
            counter++;
            if (characters.Count > counter)
            {
                characters[counter].StartTurn();
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
                    players.Remove(character);
                }
                if(character is EnemyActor)
                {
                    enemies.Remove(character);
                }
            }
        }

        private bool IsBattleOver()
        {
            if (players.Count <= 0)
            {
                return true;
            }
            if(enemies.Count <= 0)
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