using EcsCore;
using Leopotam.Ecs;
using Main.Player;
using Main.Player.Stamina;

namespace Main.Movement.Dash
{
    [EcsSystem(typeof(PlayerSetup))]
    public class DashStaminaSystem : IEcsRunSystem
    {
        private const int STAMINA_COST_PERCENT = 30;
        private EcsFilter<MovementComponent, StaminaComponent, DashComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var dash = ref _filter.Get3(i);

                if (dash.State != DashState.Start)
                    return;

                ref var stamina = ref _filter.Get2(i);

                var realCost = stamina.Max * STAMINA_COST_PERCENT * 0.01f;

                if (stamina.Current < realCost)
                {
                    _filter.GetEntity(i).Del<DashComponent>();

                    continue;
                }

                stamina.Current -= realCost;
                dash.State = DashState.Process;
            }
        }
    }
}