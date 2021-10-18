using System;
using EcsCore;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Main.Mobs
{
    [EcsSystem(typeof(MobsSetup))]
    public class MobsSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init()
        {
            var spawnPosition = new Vector3(10, 4, 18);
            var handle = Addressables.InstantiateAsync("Mob", spawnPosition, Quaternion.identity);
            handle.Completed += OnMobCreated;
        }

        private void OnMobCreated(AsyncOperationHandle<GameObject> handle)
        {
            Debug.Log("Mob is created");
        }

        public void Run()
        {
        }
    }
}