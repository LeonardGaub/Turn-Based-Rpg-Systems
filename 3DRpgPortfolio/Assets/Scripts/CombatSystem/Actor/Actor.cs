using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg.BattleSystem.Actions;

namespace Rpg.BattleSystem.Actors
{
    public class Actor : MonoBehaviour
    {
        #region Stats
        public int health;
        public int damage;
        public int speed;
        #endregion

        [SerializeField] protected ActorData data;
        public ActorData Data => data;
       
        public static Action OnFinished;
        public bool isAlive => data.Health > 0;

        private void Awake()
        {
            health = data.Health;
            damage = data.Damage;
            speed = data.Speed;
        }

        public void RecieveDamage(int dmg)
        {
            health-= dmg;
            Debug.Log("Health left: " + health);
            //Test
            if (health <= 0)
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
            print("End Turn");
            OnFinished.Invoke();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}