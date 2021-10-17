using EcsCore;
using Leopotam.Ecs;
using Main.Input;
using Main.Player;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class DashInputSystem : IEcsRunSystem
    {
        private EcsFilter<InputComponent, StaminaComponent> _filter;

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

                ref var stamina = ref _filter.Get2(i);

                if (stamina.Current < 30)
                    continue;

                stamina.Current -= 30;

                var dash = new DashComponent
                {
                    Duration = 0.1f, DelayBetween = .75f, SpeedMultiplier = 5, StaminaCost = 30
                };
                entity.Replace(dash);
            }
        }
    }
}