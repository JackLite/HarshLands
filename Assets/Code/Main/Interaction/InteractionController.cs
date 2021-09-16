using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EcsCore;
using InteractiveObjects;
using InteractiveObjects.Components;
using InteractiveObjects.GUI;
using Leopotam.Ecs;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace Main.Interaction
{
    public class InteractionController : IDisposable
    {
        private bool _isInteract;
        private ActionsPanelUI _actionsPanel;

        private readonly IEnumerable<IInteractionHandler> _interactionHandlers;
        private readonly PlayerInput _playerInput;

        public InteractionController(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.onActionTriggered += OnActionTriggered;
            EcsWorldEventsBlackboard.AddEventHandler<InteractiveObjectsCountChangeEcsEvent>(OnPlayerInteractionStateChange);
            _interactionHandlers = GetInteractionHandlers();
        }

        private void OnPlayerInteractionStateChange(InteractiveObjectsCountChangeEcsEvent _)
        {
            HideActionPanel();
        }

        private void HideActionPanel()
        {
            if (_actionsPanel)
                _actionsPanel.Hide();
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
            EcsWorldContainer.world.GetAllEntities(ref entities);
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
            
            _actionsPanel.ResetButtons();

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

        public void Dispose()
        {
            _playerInput.onActionTriggered -= OnActionTriggered;
            EcsWorldEventsBlackboard.RemoveEventHandler<InteractiveObjectsCountChangeEcsEvent>(OnPlayerInteractionStateChange);
        }
    }
}