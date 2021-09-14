using Leopotam.Ecs;

namespace Main.Interaction
{
    public interface IInteractionHandler
    {
        string InteractionName { get; }
        bool IsCanInteractWith(EcsEntity entity);
        void InteractWith(EcsEntity entity);
    }
}