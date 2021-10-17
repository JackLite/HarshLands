using System.Linq;
using EcsCore;
using Leopotam.Ecs;
using Main.Interaction;
using Main.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Input
{
    [EcsSystem(typeof(PlayerSetup))]
    public class InputReadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly PlayerInput _playerInput;
        private EcsFilter<InputComponent> _inputFilter;
        private Vector2 _currentMove;
        private bool _interact;
        private bool _dash;

        public InputReadSystem()
        {
            _playerInput = PlayerInput.GetPlayerByIndex(0);
        }

        public void Run()
        {
            ref var input = ref _inputFilter.Get1(0);
            input.Movement = _currentMove;
            input.Interact = _interact;
            input.Dash = _dash;
        }

        public void Init()
        {
            _playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case InputNames.MOVE_ACTION:
                    _currentMove = context.action.ReadValue<Vector2>();

                    return;
                case InputNames.INTERACT_ACTION:
                    _interact = context.action.ReadValue<float>() > 0;

                    return;
                case InputNames.DASH_ACTION:
                    _dash = context.action.ReadValue<float>() > 0;

                    return;
            }
        }
    }
}