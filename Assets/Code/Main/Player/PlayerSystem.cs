using Cinemachine;
using EcsCore;
using Leopotam.Ecs;
using Main.Input;
using Main.Movement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Main.Player
{
    [EcsSystem(typeof(PlayerSetup))]
    public class PlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameObject _playerMono;
        private EcsEntity _player;
        private EcsFilter<InputComponent> _inputFilter;

        public void Init()
        {
            var handle = Addressables.InstantiateAsync("Player", new Vector3(4, 4, 4), Quaternion.identity);
            handle.Completed += OnPlayerCreated;
        }
        
        private void OnPlayerCreated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Result == null)
                return;

            _playerMono = handle.Result;
            Addressables.InstantiateAsync("PlayerCamera").Completed += OnCameraCreated;
            CreatePlayer();
        }

        private void OnCameraCreated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Result == null)
                return;

            var virtualCamera = handle.Result.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = _playerMono.transform;
            virtualCamera.LookAt = _playerMono.transform;
        }

        private void CreatePlayer()
        {
            _player = EcsWorldContainer.world.NewEntity();
            AddInputComponent();
            AddMovementComponent(_playerMono);
        }

        private void AddMovementComponent(GameObject playerMono)
        {
            var movementComponent = new MovementComponent
            {
                MovementMono = playerMono.GetComponent<MovementMono>(),
                Speed = 75f,
                RotationSpeed = 25f
            };
            _player.Replace(movementComponent);
            _player.Replace(new PlayerComponent());
        }

        private void AddInputComponent()
        {
            _player.Replace(new InputComponent());
        }

        public void Run()
        {
            if (_player == EcsEntity.Null)
                return;
            
            ref var input = ref _inputFilter.Get1(0);
            ref var movement = ref _player.Get<MovementComponent>();
            movement.MovementInput = input.Movement;
        }
    }
}