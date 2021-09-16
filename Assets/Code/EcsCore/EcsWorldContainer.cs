using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Leopotam.Ecs;
using UnityEngine;

namespace EcsCore
{
    /// <summary>
    /// Отвечает за запуск ECS-мира
    /// Создаёт, обрабатывает и удаляет системы
    /// </summary>
    public class EcsWorldContainer : MonoBehaviour
    {
        private static readonly Lazy<EcsWorld> LazyWorld = new Lazy<EcsWorld>();
        public static readonly EcsWorld world = LazyWorld.Value;
        private EcsSystems _systems;

        private void Awake()
        {
            _systems = new EcsSystems(world);
            var setups = GetAllEcsSetups();

            foreach (var type in setups)
                type.Setup(_systems);

            _systems.Init();
        }

        private static IEnumerable<EcsSetup> GetAllEcsSetups()
        {
            return Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(t => t.IsSubclassOf(typeof(EcsSetup)))
                           .Select(t => (EcsSetup)Activator.CreateInstance(t));
        }

        private void Update()
        {
            _systems.Run();
            EcsWorldEventsBlackboard.Update();
        }

        private void FixedUpdate()
        {
            _systems.RunPhysics();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            world.Destroy();
        }
    }
}