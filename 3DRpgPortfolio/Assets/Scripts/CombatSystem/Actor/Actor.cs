using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private ActorData data;
        protected List<BaseAction> abilities = new List<BaseAction>();
        public static Action OnFinished;

        public ActorData Data => data;

        public bool isAlive => Data.Health > 0;

        public void RecieveDamage(int dmg)
        {
            Data.SetHealth(Data.Health - dmg);
            Debug.Log("Health left: " + Data.Health);
        }


        public void StartTurn()
        {
            StartCoroutine(Turn());
        }
        public virtual IEnumerator Turn()
        {
            yield return null;
        }

        public void EndTurn()
        {
            OnFinished.Invoke();
        }
    }
}