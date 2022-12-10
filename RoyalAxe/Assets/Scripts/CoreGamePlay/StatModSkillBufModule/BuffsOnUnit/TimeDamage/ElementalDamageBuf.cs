using System.Linq;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats
{
    public interface IBuffBehavior : IBehaviourTreeNode
    {
        
    }

    public class ElementalDamageNode : SequenceNode,IBuffBehavior
    {
        private readonly SkillEntity _buf;
        private readonly IUnitsInfluenceCalculator _influenceCalculator;

        private readonly SkillConfigDef.Damage _damage;

        private readonly TimerNode _bufDurationTimer;
        private readonly TimerNode _damageCooldownTimer;
        private readonly ActionNode _removeBuf, _damageCooldown;
        private UnitsEntity Target =>  _buf.buffTarget.Target;

        public ElementalDamageNode( SkillEntity buff,
            IUnitsInfluenceCalculator influenceCalculator,
                                  SkillConfigDef.Damage damage) : base("Магический огонь")
        {
            _buf = buff;
            _influenceCalculator = influenceCalculator;
            _damage              = damage;
            
            _damageCooldownTimer = new TimerNode(damage.DamageCooldown, "damage timer");
            _bufDurationTimer    = damage.MagicDuration <0 ? null : new TimerNode(damage.MagicDuration, "buff duration");
            _removeBuf = new ActionNode("Снимаем бафф", DoRemoveBuffTimer);
            _damageCooldown = new ActionNode("Наносим урон по кулдауну", DoDamageCooldown);
            
            BuildTree();
        }

        private bool IsTargetAlive(TimeData arg) // если цель жива
        {
            return !Target.isDeadUnit && Target.health.CurrentValue > 0;
        }
        
        
        private void BuildTree()
        {
            new BehaviourTreeBuilder().Parent(this)
                                        .Parallel("Счетчики", 0,5)
                                             .Sequence("Если цель мерта, снимаем баф")
                                                .Condition("Цель мертва", IsTargetAlive)
                                                .Do(_removeBuf)
                                             .Sequence("Таймер нанесения урона")
                                                .Do(_damageCooldownTimer)
                                                .Do(_damageCooldown)
                                        .End()
                                      .End()
                               .Build();

            if (_bufDurationTimer != null) // удаляем баф, только если есть врем действия
            {
                var child = new BehaviourTreeBuilder().Sequence("Таймер бафа")
                                                      .Do(_bufDurationTimer)
                                                      .Do(_removeBuf)
                                                      .End().Build();
                this.AddChild(child);
            }
        }

        private BehaviourTreeStatus DoRemoveBuffTimer(TimeData arg)
        {
            Target.RemoveBuf(_buf);
            return BehaviourTreeStatus.Success;
        }

        private BehaviourTreeStatus DoDamageCooldown(TimeData arg)
        {
            _damageCooldownTimer.ResetTimer();
            _influenceCalculator.ApplyElementalTimingDamage(Target, _damage.SingleDamageInfo);
            return BehaviourTreeStatus.Success;
        }

        public void IncreaseDuration(float damageMagicDuration)
        {
            _bufDurationTimer.Timer += damageMagicDuration;
        }
    }
    
    //штука, которая умеет накладывать бафы элементального урона на других юнитов
         public class ElementalBufApplyHelper : IPeriodicInfluenceApplier
        {
            public SkillConfigDef.Damage DamageData => _damage;

            private readonly SkillConfigDef.Damage _damage;
          
            private readonly IBuffFactory _buffFactory;

            public ElementalBufApplyHelper(SkillConfigDef.Damage damage, IBuffFactory buffFactory)
            {
                _damage                   = damage;
                _buffFactory = buffFactory;
            }

            public void Apply(UnitsEntity attacker, UnitsEntity target)
            {
                if (target == null || !target.isEnabled) return;
                // в списке активных бафов ищем                      элементальный урон
                var existsBuf = target.activeUnitBuff.FirstOrDefault(o => o.hasElementalDamageBuf
                                                                       && o.elementalDamageBuf.Type == _damage.ElementalDamageType // такой же как
                                                                       && o.buffOwner.Guid == attacker.uniqueUnitGUID.Guid); //и текущего персонажа
                //Важо проверять гуиды. т.к. нельзя проверить ссылки на прямую. На цели могут висеть бафы, уже умерших персонажей. 
                

                if (existsBuf != null)
                {
                    var node =  existsBuf.buffBehaviour.BehaviourTreeNode as ElementalDamageNode;
                    node?.IncreaseDuration(_damage.MagicDuration);

                }
                else
                {

                    SkillEntity buf = _buffFactory.CreateElementalBuff(attacker, _damage);

                    target.ApplyBuf(buf);
                }
            }
        }
}