using Rpg.BattleSystem.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class PlayerActor : Actor
    {
        List<EnemyActor> enemies;
        public static List<EnemyActor> availableEnemies;

        Animator anim;
        Actor target;
        BaseAction action;
        PlayerAnimations playerAnim;

        private Action OnDestinationReached;
        private bool attacking;
        Vector3 destination;

        public void SetUp(List<EnemyActor> enemies, BattleHandler battleHandler)
        {
            playerAnim = GetComponent<PlayerAnimations>();
            this.enemies = enemies;
            availableEnemies = enemies;
        }

        public override IEnumerator Turn()
        {
            print("Player turn");
            availableEnemies = enemies.Where(val => val.isAlive).ToList();
            if (availableEnemies.Count <= 0)
            {
                EndTurn();
                yield break;
            }
            yield return new WaitUntil(() => action != null && target != null);
            Attack(() => 
            {
                ClearAttack();
                EndTurn();  });
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

                    float reachedDistance = 1f;
                    if (Vector3.Distance(GetPosition(), destination) < reachedDistance)
                    {
                        OnDestinationReached();
                    }
                }
                else if (action.attackStartPoint == AttackStartPoint.OnSpot)
                {
                    OnDestinationReached();
                }
            }
        }

        private void Attack(Action OnAttackComplete)
        {
            Vector3 slideTargetPosition = target.GetPosition() - new Vector3(0,0, 1);
            print(slideTargetPosition);
            Vector3 startingPosition = transform.position;

            SlideToPosition(slideTargetPosition, () => {
                attacking = false;
                playerAnim.PlayAnimation(action.animTrigger, () => {
                    action.Execute(this, target);
                    SlideToPosition(startingPosition, () => {
                        attacking = false;
                        //characterBase.PlayAnimIdle(attackDir);
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


        public void ClearAttack()
        {
            target = null;
            action = null;
        }

        public void OnActionChoose(BaseAction action)
        {
            this.action = action;
        }

        public void OnTargetChoose(Actor target)
        {
            this.target = target;
        }
    }
}
