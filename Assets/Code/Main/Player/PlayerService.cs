using System;
using System.Linq;
using EcsCore;
using Leopotam.Ecs;

namespace Main.Player
{
    public static class PlayerService
    {
        public static bool IsPlayerMoving()
        {
            var entities = Array.Empty<EcsEntity>();
            EcsWorldContainer.world.GetAllEntities(ref entities);
            var playerEntity = entities.FirstOrDefault(e => e.Has<PlayerComponent>());

            return playerEntity.Id > 0 && playerEntity.Get<PlayerComponent>().IsMoving;
        }
    }
}