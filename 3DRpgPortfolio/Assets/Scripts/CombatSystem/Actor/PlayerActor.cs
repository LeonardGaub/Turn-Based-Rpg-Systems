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
        BattleHandler battleHandler;
        List<EnemyActor> enemies;
        public static List<EnemyActor> availableEnemies;

        Animator anim;
        Actor target;
        BaseAction action;

        private bool attacking;

        public void SetUp(List<EnemyActor> enemies, BattleHandler battleHandler)
        {
            anim = GetComponent<Animator>();
            this.enemies = enemies;
            availableEnemies = enemies;
            this.battleHandler = battleHandler;
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
            attacking = true;
        }

        private void Update()
        {
            if (action == null || target == null || attacking == false) { return; }
            switch (action.attackStartPoint)
            {
                case AttackStartPoint.OnEnemy:
                    break;
                case AttackStartPoint.OnSpot:
                    anim.SetTrigger(action.animTrigger);
                    attacking = false;
                    break;
            }
        }

        private void Attack()
        {
            action.Execute(this, target);
        }

        private void AnimationFinished()
        {
            Attack();
            ClearAttack();
            EndTurn();
        }


        public void ClearAttack()
        {
            target = null;
            action = null;
        }

        public void OnActionChoose(BaseAction action)
        {
            print("Action");
            this.action = action;
            print(action);
        }

        public void OnTargetChoose(Actor target)
        {
            print("Target");
            this.target = target;
            print(target);
        }
    }
}
