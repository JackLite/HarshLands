using InteractiveObjects.Components;
using Leopotam.Ecs;
using Main;

namespace InteractiveObjects
{
    [EcsSystem(typeof(TempInteractiveObjectsSetup))]
    public class InteractiveObjectSystem : IEcsRunSystem
    {
        private EcsFilter<InteractiveObjectComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var io = ref _filter.Get1(i);
                io.Mono.SetInteraction(io.IsInteractionPossible);
            }
        }
    }
}