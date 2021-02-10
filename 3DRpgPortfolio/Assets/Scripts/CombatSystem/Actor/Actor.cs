using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg.BattleSystem.Actions;
using System.Linq;

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
        public bool isAlive => health > 0;

        protected Actor target;
        protected BaseAction action;
        
        
        BattleAnimations anim;
        private Action OnDestinationReached;
        private bool attacking;
        Vector3 destination;

        public void SetUp()
        {
            health = data.Health;
            damage = data.Damage;
            speed = data.Speed;

            anim = GetComponent<BattleAnimations>();
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
            anim.PlayAnimation("GetHit");
            if (health <= 0)
            {
                gameObject.SetActive(false);
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
                anim.PlayAnimation(action.animTrigger, () =>
                {
                    action.Execute(this, target);
                }, () =>
                {
                    SlideToPosition(startingPosition, () =>
                    {
                        attacking = false;
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
            if (destination.x > 0)
            {
                //characterBase.PlayAnimSlideRight();
            }
            else
            {
                //characterBase.PlayAnimSlideLeft();
            }
        }

        protected void ClearAttack()
        {
            action = null;
            target = null;
        }
    }
}