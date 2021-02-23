using Rpg.BattleSystem.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public abstract class ActorData : ScriptableObject
    {
        [Header("Base/Max Stats")]
        [SerializeField] private int baseHealth;
        [SerializeField] private int baseDamage;
        [SerializeField] private int baseSpeed;
        public int maxHealth;

        [Space]
        [Header("Current Stats")]
        public int health;
        public int damage;
        public int speed;

        
        [Space]
        [SerializeField] List<BaseAction> abilities = new List<BaseAction>();
        public Vector3 offset = new Vector3(0, 0, 0);
        public List<BaseAction> Abilities => abilities;
        public string objectiveString;

        public void SetStats(int hp, int dmg, int sp)
        {
            health = hp;
            damage = dmg;
            speed = sp;
        }

        public void Reset()
        {
            health = baseHealth;
            damage = baseDamage;
            speed = baseSpeed;
        }

        public void AddStats(int hp, int dmg, int sp)
        {
            maxHealth += hp;
            damage += dmg;
            speed += sp;
        }

        public void RemoveStats(int hp, int dmg, int sp)
        {
            maxHealth -= hp;
            damage -= dmg;
            speed -= sp;
        }
    }
}
