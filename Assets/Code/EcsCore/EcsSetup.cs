using System;
using Leopotam.Ecs;

namespace EcsCore
{
    /// <summary>
    /// Базовый класс для всех точек создания ECS-систем
    /// </summary>
    public abstract class EcsSetup
    {
        protected abstract Type Type { get; }

        /// <summary>
        /// Создаёт системы и добавляет их в набор
        /// </summary>
        /// <param name="systems">Набор ECS-систем</param>
        public void Setup(in EcsSystems systems)
        {
            foreach (var system in EcsUtilities.CreateSystems(Type))
                systems.Add(system);
        }
    }
}