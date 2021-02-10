using Rpg.BattleSystem.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public abstract class ActorData : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int defense;
        [SerializeField] private int damage;
        [SerializeField] private int speed;
        [SerializeField] List<BaseAction> abilities = new List<BaseAction>();
        public Vector3 offset = new Vector3(0, 0, 0);
        public int Health => health;
        public int Defense => defense;
        public int Damage => damage;
        public int Speed => speed;
        public List<BaseAction> Abilities => abilities;

        public void SetHealth(int health)
        {
            this.health = health;
        }
    }
}
