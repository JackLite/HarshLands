using Leopotam.Ecs;
using UnityEngine;

namespace Main
{
    [EcsSystem]
    public class MovementSystem : IEcsPhysicRunSystem
    {
        private EcsFilter<MovementComponent> _filter;
        public void RunPhysics()
        {
            foreach (var index in _filter)
            {
                ref var movement = ref _filter.Get1(index);
                movement.MovementMono.Move(InputReader.Force);
            }
        }
    }
}