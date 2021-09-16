using Leopotam.Ecs;

namespace Main.Interaction
{
    /// <summary>
    /// Обработчик взаимодействия с объектом
    /// Отвечает за обработку клика по соответствующему взаимодействию с объектом
    /// </summary>
    public interface IInteractionHandler
    {
        string InteractionName { get; }
        bool IsCanInteractWith(EcsEntity entity);
        void InteractWith(EcsEntity entity);
    }
}