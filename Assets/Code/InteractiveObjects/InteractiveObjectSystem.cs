using EcsCore;
using InteractiveObjects.Components;
using Leopotam.Ecs;
using Main;

namespace InteractiveObjects
{
    [EcsSystem(typeof(TempInteractiveObjectsSetup))]
    public class InteractiveObjectSystem : IEcsRunSystem
    {
        private EcsFilter<InteractiveObjectComponent> _filter;
        private int _previousCount;

        public void Run()
        {
            var count = 0;

            foreach (var i in _filter)
            {
                ref var io = ref _filter.Get1(i);
                io.Mono.SetInteraction(io.IsInteractionPossible);
                if (io.IsInteractionPossible)
                    count++;
            }

            if (count != _previousCount)
            {
                EcsWorldEventsBlackboard.AddEvent(new InteractiveObjectsCountChangeEcsEvent());
            }

            _previousCount = count;
        }
    }
}