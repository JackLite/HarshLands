using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Leopotam.Ecs;

namespace Main
{
    public static class EcsUtilities
    {
        public static IEnumerable<IEcsSystem> CreateSystems(Type setupType)
        {
            return
                from type in Assembly.GetExecutingAssembly().GetTypes()
                let attr = type.GetCustomAttribute<EcsSystemAttribute>()
                where attr != null && attr.Startup == setupType
                select (IEcsSystem) Activator.CreateInstance(type);
        }
    }
}