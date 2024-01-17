using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_Team
{
    internal class SkillAbility
    {
        protected ICharacter attacker;

        public virtual void OnAttack(ICharacter character, int damage)
        {

        }

        public virtual void OnAttackStart()
        {

        }

        public virtual void OnAttackEnd()
        {

        }

        public ICharacter Attacker
        {
            get { return attacker; }
            set { attacker = value; }
        }
    }

    internal class SkillAbility_Nothing : SkillAbility
    {
        public override void OnAttackStart()
        {
            base.OnAttackStart();
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name}의 스킬 시작 시 발동합니다.");
            Console.WriteLine();
        }

        public override void OnAttack(ICharacter victim, int damage)
        {
            base.OnAttack(victim, damage);
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name}의 스킬 어빌리티 테스트입니다.");
            Console.WriteLine();
        }

        public override void OnAttackEnd()
        {
            base.OnAttackEnd();
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name}의 스킬이 끝날 때 발동합니다.");
            Console.WriteLine();
        }
    }

    internal class SkillAbility_LifeSteal : SkillAbility
    {

        public override void OnAttack(ICharacter victim, int damage)
        {
            base.OnAttack(victim, damage);
            int health = attacker.Health;
            attacker.Health = (attacker.MaxHealth < attacker.Health + damage / 2) ? attacker.MaxHealth : attacker.Health + damage / 2;
            Console.WriteLine();
            Console.WriteLine($"{attacker.Name}의 체력이 회복됩니다.");

            Console.WriteLine($"Lv.{attacker.Level} {attacker.Name}");
            Console.WriteLine($"HP {health} -> {attacker.Health}");
            Console.WriteLine();
        }
    }
}
