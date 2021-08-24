using System;
using Leopotam.Ecs;

namespace Main
{
    public abstract class EcsSetup
    {
        protected abstract Type Type { get; }

        public virtual void Setup(in EcsSystems systems)
        {
            foreach (var system in EcsUtilities.CreateSystems(Type))
                systems.Add(system);
        }
    }
}