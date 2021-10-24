using EcsCore;
using Leopotam.Ecs;
using Main.Input;
using Main.Movement;
using Main.Movement.Dash;

namespace Main.Player
{
    [EcsSystem(typeof(PlayerSetup))]
    public class PlayerDashInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsEntity _inputEntity;
        private EcsEntity _player;
        private EcsFilter<PlayerTag> _filter;

        public void Init()
        {
            _inputEntity = EcsWorldContainer.world.NewEntity();
            _inputEntity.Replace(new InputEventComponent
            {
                Action = InputNames.DASH_ACTION
            });
        }

        public void Run()
        {
            _player = _filter.GetEntity(0);

            if (_inputEntity.Get<InputEventComponent>().State == InputStateEnum.None)
                return;

            if (_player.Has<DashComponent>())
                return;

            var dash = new DashComponent
            {
                Duration = 0.1f, DelayBetween = .75f, SpeedMultiplier = 5
            };

            _player.Replace(dash);
        }
    }
}