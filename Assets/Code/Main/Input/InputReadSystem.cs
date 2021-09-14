using System.Linq;
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

        private const string MOVE_ACTION = "Move";

        public InputReadSystem()
        {
            _playerInput = PlayerInput.GetPlayerByIndex(0);
        }

        public void Run()
        {
            ref var input = ref _inputFilter.Get1(0);
            input.Movement = _currentMove;
            input.Interact = _interact;
        }

        public void Init()
        {
            _playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case MOVE_ACTION:
                    _currentMove = context.action.ReadValue<Vector2>();

                    return;
                case "Interact":
                    _interact = context.action.ReadValue<float>() > 0;

                    return;
            }
        }
    }
}