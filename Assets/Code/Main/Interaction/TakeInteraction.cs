using InteractiveObjects.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Main.Interaction
{
    public class TakeInteraction : IInteractionHandler
    {
        public string InteractionName => "Take";

        public bool IsCanInteractWith(EcsEntity entity)
        {
            return entity.Has<TakeComponent>();
        }

        public void InteractWith(EcsEntity entity)
        {
            Debug.Log($"Take {entity.Get<InteractiveObjectComponent>().Mono.name}");
        }
    }
}