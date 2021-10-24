namespace Main.Movement.Dash
{
    public struct DashComponent
    {
        public float SpeedMultiplier;
        public float Duration;
        public float DelayBetween;
        public float CurrentDuration;
        public float CurrentDelay;
        public DashState State;
    }
}