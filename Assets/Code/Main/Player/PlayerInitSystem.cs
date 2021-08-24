﻿using Cinemachine;
using Leopotam.Ecs;
using Main.Input;
using Main.Movement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Main.Player
{
    [EcsSystem(typeof(PlayerSetup))]
    public class PlayerInitSystem : IEcsInitSystem
    {
        private GameObject _playerMono;

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
            var player = EcsWorldStartup.World.NewEntity();
            AddInputComponent(player);
            AddMovementComponent(player, _playerMono);
        }

        private static void AddMovementComponent(EcsEntity player, GameObject playerMono)
        {
            var movementComponent = new MovementComponent
            {
                MovementMono = playerMono.GetComponent<MovementMono>(),
                Speed = 75f,
                RotationSpeed = 25f
            };
            player.Replace(movementComponent);
        }

        private static void AddInputComponent(EcsEntity player)
        {
            player.Replace(new InputComponent());
        }
    }
}