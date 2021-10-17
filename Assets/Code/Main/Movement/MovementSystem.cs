using EcsCore;
using Leopotam.Ecs;
using Main.Player;
using UnityEngine;

namespace Main.Movement
{
    [EcsSystem(typeof(PlayerSetup))]
    public class MovementSystem : IEcsPhysicRunSystem
    {
        private EcsFilter<MovementComponent> _filter;

        public void RunPhysics()
        {
            if (_filter.GetEntitiesCount() == 0) 
                return;
            ref var movement = ref _filter.Get1(0);
            var input = movement.MovementInput;
            if (input == Vector2.zero)
                return;

            var direction = new Vector3(input.x, 0, input.y);
            movement.MovementMono.Move(direction, movement.Speed * movement.SpeedMultiplier);
            movement.MovementMono.Rotate(direction, movement.RotationSpeed);
        }
    }
}