using Leopotam.Ecs;
using Main.Input;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem]
    public class MovementSystem : IEcsPhysicRunSystem
    {
        private EcsFilter<MovementComponent> _filter;
        private EcsFilter<InputComponent> _inputFilter;
        public void RunPhysics()
        {
            ref var input = ref _inputFilter.Get1(0);
            foreach (var index in _filter)
            {
                ref var movement = ref _filter.Get1(index);
                movement.MovementMono.Move(new Vector3(input.Movement.x, 0, input.Movement.y));
            }
        }
    }
}