using Leopotam.Ecs;
using Main.Player;
using UnityEngine.InputSystem;

namespace Main.Interaction
{
    [EcsSystem(typeof(PlayerSetup))]
    public class InteractionSystem : IEcsInitSystem
    {
        private InteractionController _controller;

        public void Init()
        {
            _controller = new InteractionController(PlayerInput.GetPlayerByIndex(0));
        }
    }
}