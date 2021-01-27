using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class BattleHandler : MonoBehaviour
    {
        List<Actor> players = new List<Actor>();
        List<Actor> enemies = new List<Actor>();
        enum BattleState
        {
            WaitingForInput,
            Busy
        }
        public static BattleHandler instance;

        private BattleState state;

        private void Awake()
        {
            instance = this;
            players.Add(new Actor());
            players.Add(new Actor());
            players.Add(new Actor());
        }

        private void Start()
        {
            StartCoroutine(PlayerTurn());
        }

        private void Update()
        {
            switch (state)
            {
                case BattleState.WaitingForInput:
                    break;
                case BattleState.Busy:
                    break;
            }
        }

        private IEnumerator PlayerTurn()
        {
            foreach (Actor player in players)
            {
                yield return null;
                //yield return player.StartCoroutine(player.ActionFinshed());
            }
        }

        public void StartBattle(Actor[] players, Actor[] enemies)
        {
            state = BattleState.WaitingForInput;
            this.players = players.ToList();
            this.enemies = enemies.ToList();
        }
    }
}