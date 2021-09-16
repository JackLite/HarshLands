using EcsCore;
using Leopotam.Ecs;
using Main.Player;
using UnityEngine.InputSystem;

namespace Main.Interaction
{
    [EcsSystem(typeof(PlayerSetup))]
    public class InteractionSystem : IEcsInitSystem, IEcsPostDestroySystem
    {
        private InteractionController _controller;

        public void Init()
        {
            _controller = new InteractionController(PlayerInput.GetPlayerByIndex(0));
        }

        public void PostDestroy()
        {
            _controller.Dispose();
        }
    }
}