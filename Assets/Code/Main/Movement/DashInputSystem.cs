using EcsCore;
using Leopotam.Ecs;
using Main.Input;
using Main.Player;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class DashInputSystem : IEcsRunSystem
    {
        private EcsFilter<InputComponent> _filter;

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

                var dash = new DashComponent
                {
                    Duration = 0.1f, DelayBetween = .75f, SpeedMultiplier = 5
                };
                entity.Replace(dash);
            }
        }
    }
}