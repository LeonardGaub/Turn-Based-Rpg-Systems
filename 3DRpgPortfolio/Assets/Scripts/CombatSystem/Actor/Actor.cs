using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg.BattleSystem.Actions;

namespace Rpg.BattleSystem.Actors
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] protected ActorData data;
        public ActorData Data => data;
       
        public static Action OnFinished;

        public bool isAlive => data.Health > 0;

        public void RecieveDamage(int dmg)
        {
            data.SetHealth(data.Health - dmg);
            Debug.Log("Health left: " + data.Health);
            //Test
            if (data.Health <= 0)
            {
                gameObject.SetActive(false);
            }
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