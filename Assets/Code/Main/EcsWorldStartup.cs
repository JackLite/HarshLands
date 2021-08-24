using System;
using System.Linq;
using System.Reflection;
using Leopotam.Ecs;
using UnityEngine;

namespace Main
{
    public class EcsWorldStartup : MonoBehaviour
    {
        private static readonly Lazy<EcsWorld> LazyWorld = new Lazy<EcsWorld>();
        public static readonly EcsWorld World = LazyWorld.Value;
        private EcsSystems _systems;

        private void Awake()
        {
            _systems = new EcsSystems(World);
            var setups = Assembly.GetExecutingAssembly()
                                 .GetTypes()
                                 .Where(t => t.IsSubclassOf(typeof(EcsSetup)))
                                 .Select(t => (EcsSetup)Activator.CreateInstance(t))
                                 .ToArray();

            foreach (var type in setups)
                type.Setup(_systems);

            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void FixedUpdate()
        {
            _systems.RunPhysics();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            World.Destroy();
        }
    }
}