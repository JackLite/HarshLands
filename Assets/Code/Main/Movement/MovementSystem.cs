using Leopotam.Ecs;
using Main.Input;
using Main.Player;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class MovementSystem : IEcsPhysicRunSystem
    {
        private EcsFilter<MovementComponent> _filter;
        private EcsFilter<InputComponent> _inputFilter;

        public void RunPhysics()
        {
            ref var input = ref _inputFilter.Get1(0);

            if (input.Movement == Vector2.zero) return;

            foreach (var index in _filter)
            {
                ref var movement = ref _filter.Get1(index);
                var direction = new Vector3(input.Movement.x, 0, input.Movement.y);
                movement.MovementMono.Move(direction, movement.Speed);
                movement.MovementMono.Rotate(direction, movement.RotationSpeed);
            }
        }
    }
}