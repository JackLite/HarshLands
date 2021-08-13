﻿using System;
using System.Linq;
using System.Reflection;
using Leopotam.Ecs;
using UnityEngine;

namespace Main
{
    public class EcsWorldStartup : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        private EcsWorld _world;
        private EcsSystems _systems;

        public static EcsWorld World;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            var systems = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => t.GetCustomAttribute<EcsSystemAttribute>() != null);

            foreach (var system in systems) 
                _systems.Add((IEcsSystem)Activator.CreateInstance(system));

            _systems.Init();
            var playerEntity = _world.NewEntity();
            playerEntity.Replace(new MovementComponent
            {
                Transform = player.transform, MovementMono = player.GetComponent<MovementMono>()
            });
        }

        private void Update()
        {
            _systems.Run();
        }

        private float _lastTime;

        private void FixedUpdate()
        {
            Debug.Log("ecs " + (Time.time - _lastTime));
            _lastTime = Time.time;
            _systems.RunPhysics();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}