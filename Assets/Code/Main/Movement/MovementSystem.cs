using Leopotam.Ecs;
using Main.Input;
using Main.Player;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class MovementSystem : IEcsPhysicRunSystem
    {
        private EcsFilter<InputComponent> _inputFilter;
        private EcsFilter<PlayerComponent, MovementComponent> _playerFilter;

        public void RunPhysics()
        {
            if (_playerFilter.GetEntitiesCount() == 0) 
                return;
            ref var input = ref _inputFilter.Get1(0);

            _playerFilter.Get1(0).IsMoving = false;
            if (input.Movement == Vector2.zero)
                return;

            ref var movement = ref _playerFilter.Get2(0);
            _playerFilter.Get1(0).IsMoving = true;
            var direction = new Vector3(input.Movement.x, 0, input.Movement.y);
            movement.MovementMono.Move(direction, movement.Speed);
            movement.MovementMono.Rotate(direction, movement.RotationSpeed);
        }
    }
}