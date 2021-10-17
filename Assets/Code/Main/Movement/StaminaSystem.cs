using EcsCore;
using Leopotam.Ecs;
using Main.Player;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class StaminaSystem : IEcsRunSystem
    {
        private EcsFilter<StaminaComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var stamina = ref _filter.Get1(i);

                stamina.Current += stamina.RestorePerSecond * Time.deltaTime;

                if (stamina.Max - stamina.Current <= 0.001f)
                {
                    stamina.Current = stamina.Max;
                }
                
                stamina.Mono.UpdateStamina(stamina.Current);
            }
        }
    }
}