using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg.BattleSystem.Actions;
using System.Linq;
using Rpg.Saving;

namespace Rpg.BattleSystem.Actors
{
    public class Actor : MonoBehaviour, ISaveable
    {
        #region Stats
        public int health;
        public int damage;
        public int defense;
        public int speed;
        #endregion

        [SerializeField] protected ActorData data;
        
        public ActorData Data => data;

        public static Action OnFinished;
        public bool isAlive => health > 0;

        protected Actor target;
        protected BaseAction action;
        
        
        BattleAnimations anim;
        private Action OnDestinationReached;
        private bool attacking;
        Vector3 destination;

        public void SetUp()
        {
            anim = GetComponent<BattleAnimations>();
        }

        private void SetStats(int hp, int dmg, int def, int sp)
        {
            health = hp;
            damage = dmg;
            defense = def;
            speed = sp;
        }

        private void Update()
        {
            if (action == null || target == null || attacking == false) { return; }
            if (attacking)
            {
                if (action.attackStartPoint == AttackStartPoint.OnEnemy)
                {

                    float slideSpeed = 10f;
                    transform.position += (destination - transform.position) * slideSpeed * Time.deltaTime;

                    float reachedDistance = 0.1f;
                    if (Vector3.Distance(GetPosition(), destination) < reachedDistance)
                    {
                        transform.position = destination;
                        OnDestinationReached();
                    }
                }
                else if (action.attackStartPoint == AttackStartPoint.OnSpot)
                {
                    OnDestinationReached();
                }
            }
        }


        public void RecieveDamage(int dmg)
        {
            health-= dmg;
            
            if (health <= 0)
            {
                anim.PlayAnimation("Death", () => { gameObject.SetActive(false); });
            }
            else
            {
                anim.PlayAnimation("GetHit");
            }
        }

        public void StartTurn()
        {
            if (!isAlive) { EndTurn(); return; }
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

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        protected void Attack(Action OnAttackComplete)
        {
            Vector3 slideTargetPosition = target.GetPosition() - data.offset;
            Vector3 startingPosition = GetPosition();
            SlideToPosition(slideTargetPosition, () =>
            {
                attacking = false;
                anim.PlayAnimation(attacking, "Attacking");
                anim.PlayAnimation(action.animTrigger, () =>
                {
                    action.Execute(this, target);
                }, () =>
                {
                    SlideToPosition(startingPosition, () =>
                    {
                        attacking = false;
                        anim.PlayAnimation(attacking, "Attacking");
                        OnAttackComplete.Invoke();
                    });
                });
            });
        }
        private void SlideToPosition(Vector3 destination, Action OnDestinationReached)
        {
            this.destination = destination;
            this.OnDestinationReached = OnDestinationReached;
            attacking = true;
            anim.PlayAnimation(attacking, "Attacking");
        }

        protected void ClearAttack()
        {
            action = null;
            target = null;
        }

        public object CaptureState()
        {
            return new ActorStats(health, damage, defense, speed);
        }

        public void RestoreState(object state)
        {
            var stats = state as ActorStats;
            if(stats != null)
            {
                SetStats(stats.health, stats.damage, stats.defense, stats.speed);
            }
        }
    }
}
[Serializable]
public class ActorStats
{
    public int health;
    public int damage;
    public int defense;
    public int speed;

    public ActorStats(int hp, int dmg, int def, int sp)
    {
        health = hp;
        damage = dmg;
        defense = def;
        speed = sp;
    }
}