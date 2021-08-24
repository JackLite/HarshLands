using InteractiveObjects.Components;
using Leopotam.Ecs;
using Main;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InteractiveObjects
{
    [EcsSystem(typeof(TempInteractiveObjectsSetup))]
    public class TempObjectInitSystem : IEcsInitSystem
    {
        public void Init()
        {
            var handle = Addressables.InstantiateAsync("InteractiveObject", new Vector3(15, 1, 17), Quaternion.identity);
            handle.Completed += OnGameObjectCreated;
            
            var handle2 = Addressables.InstantiateAsync("InteractiveObject", new Vector3(6, 1, 6), Quaternion.identity);
            handle2.Completed += OnGameObjectCreated;
            
            var handle3 = Addressables.InstantiateAsync("InteractiveObject", new Vector3(13, 1, 15), Quaternion.identity);
            handle3.Completed += OnGameObjectCreated;
        }

        private void OnGameObjectCreated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Result == null) 
                return;

            var interactiveObj = EcsWorldStartup.World.NewEntity();
            var mono = handle.Result.GetComponent<InteractiveObjectMono>();

            interactiveObj.Replace(new InteractiveObjectComponent() {Mono = mono});

            interactiveObj.Replace(new DestroyableComponent
            {
                Health = 100
            });
            interactiveObj.Replace(new TakeComponent());

            mono.OnPlayerEnter += () => interactiveObj.Get<InteractiveObjectComponent>().IsInteractionPossible = true;
            mono.OnPlayerExit += () => interactiveObj.Get<InteractiveObjectComponent>().IsInteractionPossible = false;
        }
    }
}