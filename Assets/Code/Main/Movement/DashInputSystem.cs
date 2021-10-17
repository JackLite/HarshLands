using EcsCore;
using Leopotam.Ecs;
using Main.Input;
using Main.Player;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class DashInputSystem : IEcsRunSystem
    {
        private EcsFilter<InputComponent, MovementComponent, StaminaComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);

                if (entity.Has<DashComponent>())
                    continue;

                ref var input = ref _filter.Get1(i);

                if (!input.Dash)
                    continue;

                ref var movement = ref _filter.Get2(i);

                if (movement.MovementInput == Vector2.zero)
                    continue;
                
                ref var stamina = ref _filter.Get3(i);

                const int STAMINA_COST_PERCENT = 30;
                var realCost = stamina.Max * STAMINA_COST_PERCENT * 0.01f;
                if (stamina.Current < realCost)
                    continue;

                stamina.Current -= realCost;

                var dash = new DashComponent
                {
                    Duration = 0.1f, DelayBetween = .75f, SpeedMultiplier = 5
                };
                entity.Replace(dash);
            }
        }
    }
}