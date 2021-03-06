using EcsCore;
using Leopotam.Ecs;
using Main.Player;
using UnityEngine;

namespace Main.Movement.Dash
{
    [EcsSystem(typeof(PlayerSetup))]
    public class DashSystem : IEcsRunSystem
    {
        private EcsFilter<MovementComponent, DashComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movement = ref _filter.Get1(i);
                ref var dash = ref _filter.Get2(i);

                if (movement.MovementInput == Vector2.zero && dash.State == DashState.Process)
                {
                    movement.MovementInput = movement.MovementMono.GetForward();
                }

                if (dash.State == DashState.Start)
                    continue;

                var isDashActive = dash.CurrentDuration > 0 || dash.CurrentDelay > 0;

                if (!isDashActive)
                {
                    movement.SpeedMultiplier = dash.SpeedMultiplier;
                    dash.CurrentDuration = dash.Duration;

                    return;
                }

                dash.CurrentDuration -= Time.deltaTime;

                if (dash.CurrentDuration <= 0 && dash.CurrentDelay <= 0)
                {
                    movement.SpeedMultiplier = 1;
                    dash.CurrentDelay = dash.DelayBetween;
                    dash.State = DashState.Restore;
                }

                dash.CurrentDelay -= Time.deltaTime;

                if (dash.CurrentDuration <= 0 && dash.CurrentDelay <= 0)
                {
                    _filter.GetEntity(i).Del<DashComponent>();
                }
            }
        }
    }
}