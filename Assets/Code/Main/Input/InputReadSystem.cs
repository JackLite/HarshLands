using System.Linq;
using Leopotam.Ecs;
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

        private const string INPUT_ACTION = "Move";

        public InputReadSystem()
        {
            _playerInput = PlayerInput.all.First();
        }

        public void Run()
        {
            ref var input = ref _inputFilter.Get1(0);
            input.Movement = _currentMove;
        }

        public void Init()
        {
            _playerInput.onActionTriggered += context =>
            {
                if (context.action.name == INPUT_ACTION) _currentMove = context.action.ReadValue<Vector2>();
            };
        }
    }
}