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

        public int Health => health;
        public int Defense => defense;
        public int Damage => damage;
        public int Speed => speed;

        public void SetHealth(int health)
        {
            this.health = health;
        }
    }
}
