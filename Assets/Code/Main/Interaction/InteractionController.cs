using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InteractiveObjects.Components;
using InteractiveObjects.GUI;
using Leopotam.Ecs;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace Main.Interaction
{
    public class InteractionController
    {
        private bool _isInteract;
        private ActionsPanelUI _actionsPanel;

        private readonly IEnumerable<IInteractionHandler> _interactionHandlers;

        public InteractionController(PlayerInput playerInput)
        {
            playerInput.onActionTriggered += OnActionTriggered;
            _interactionHandlers = GetInteractionHandlers();
        }

        private static IEnumerable<IInteractionHandler> GetInteractionHandlers()
        {
            return
                from type in Assembly.GetExecutingAssembly().GetTypes()
                let interfaces = type.GetInterfaces()
                where interfaces.Contains(typeof(IInteractionHandler))
                select Activator.CreateInstance(type) as IInteractionHandler;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (context.action.name != "Interact")
                return;

            var interact = context.action.ReadValue<float>() > 0;

            if (_isInteract == interact)
                return;

            _isInteract = interact;

            if (!_isInteract)
                return;

            var interactionObjects = GetInteractionObjects();

            if (interactionObjects.Length == 1)
            {
                ShowUI(interactionObjects[0]);
            }
        }

        private static EcsEntity[] GetInteractionObjects()
        {
            var entities = Array.Empty<EcsEntity>();
            EcsWorldStartup.World.GetAllEntities(ref entities);
            var interactionObjects = entities.Where(IsCanInteractWithEntity).ToArray();

            return interactionObjects;
        }

        private async void ShowUI(EcsEntity interactionObject)
        {
            if (_actionsPanel == null)
            {
                var task = Addressables.InstantiateAsync("InteractionPanel").Task;
                await task;
                _actionsPanel = task.Result.GetComponent<ActionsPanelUI>();
            }

            _actionsPanel.Hide();

            foreach (var handler in _interactionHandlers)
            {
                if (handler.IsCanInteractWith(interactionObject))
                    _actionsPanel.AddButton(handler.InteractionName, () => handler.InteractWith(interactionObject));
            }

            var interactionMono = interactionObject.Get<InteractiveObjectComponent>().Mono;
            _actionsPanel.ShowAtObject(interactionMono.transform);
        }

        private static bool IsCanInteractWithEntity(EcsEntity entity)
        {
            return entity.Has<InteractiveObjectComponent>()
                   && entity.Get<InteractiveObjectComponent>().IsInteractionPossible;
        }
    }
}