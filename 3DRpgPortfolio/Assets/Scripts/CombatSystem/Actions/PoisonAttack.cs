using Rpg.BattleSystem.Actors;
using Rpg.BattleSystem.Effects;

namespace Rpg.BattleSystem.Actions
{
    public class PoisonAttack : BaseAction
    {
        public override void Execute(Actor user, Actor target)
        {
            target.RecieveDamage(user.Data.damage);
            target.AddEffect(new PoisonEffect(user.Data.damage / 4, 2));
        }
    }
}
