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
        private const int START_PANELS_COUNT = 4;

        private bool _isInteract;
        
        private readonly List<ActionsPanelUI> _actionsPanels = new List<ActionsPanelUI>();
        private readonly Queue<ActionsPanelUI> _availableActionsPanels = new Queue<ActionsPanelUI>();

        private readonly IEnumerable<IInteractionHandler> _interactionHandlers;
        private readonly PlayerInput _playerInput;

        public InteractionController(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.onActionTriggered += OnActionTriggered;
            EcsWorldEventsBlackboard.AddEventHandler<InteractiveObjectsCountChangeEcsEvent>(OnPlayerInteractionStateChange);
            _interactionHandlers = GetInteractionHandlers();
            for (var i = START_PANELS_COUNT; i > 0; i--)
                CreatePanel();
        }

        private async void CreatePanel()
        {
            var task = Addressables.InstantiateAsync("InteractionPanel").Task;
            await task;

            if (task.Result == null)
                throw new Exception("Can't create UI actions panel");
            var actionsPanelUI = task.Result.GetComponent<ActionsPanelUI>();
            _actionsPanels.Add(actionsPanelUI);
            _availableActionsPanels.Enqueue(actionsPanelUI);
        }

        private void OnPlayerInteractionStateChange(InteractiveObjectsCountChangeEcsEvent _)
        {
            HideActionPanel();
        }

        private void HideActionPanel()
        {
            _availableActionsPanels.Clear();
            foreach (var actionsPanel in _actionsPanels)
            {
                actionsPanel.Hide();
                _availableActionsPanels.Enqueue(actionsPanel);
            }
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

            foreach (var interactionObject in interactionObjects)
            {
                ShowUI(interactionObject);
            }
        }

        private static IEnumerable<EcsEntity> GetInteractionObjects()
        {
            var entities = Array.Empty<EcsEntity>();
            EcsWorldContainer.world.GetAllEntities(ref entities);
            var interactionObjects = entities.Where(IsCanInteractWithEntity).ToArray();

            return interactionObjects;
        }

        private void ShowUI(EcsEntity interactionObject)
        {
            var panel = _availableActionsPanels.Dequeue();
            foreach (var handler in _interactionHandlers)
            {
                if (handler.IsCanInteractWith(interactionObject))
                    panel.AddButton(handler.InteractionName, () => handler.InteractWith(interactionObject));
            }

            var interactionMono = interactionObject.Get<InteractiveObjectComponent>().Mono;
            panel.ShowAtObject(interactionMono.transform);
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