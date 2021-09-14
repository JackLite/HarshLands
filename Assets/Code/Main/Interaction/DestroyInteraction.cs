using InteractiveObjects.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Main.Interaction
{
    public class DestroyInteraction : IInteractionHandler
    {
        public string InteractionName => "Destroy";
        public bool IsCanInteractWith(EcsEntity entity)
        {
            return entity.Has<DestroyableComponent>();
        }

        public void InteractWith(EcsEntity entity)
        {
            Debug.Log($"Destroy {entity.Get<InteractiveObjectComponent>().Mono.name}");
        }
    }
}