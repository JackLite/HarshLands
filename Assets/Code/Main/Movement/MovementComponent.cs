using UnityEngine;

namespace Main.Movement
{
    public struct MovementComponent
    {
        public Vector2 MovementInput;
        public float Speed;
        public float SpeedMultiplier;
        public float RotationSpeed;
        public MovementMono MovementMono;
    }
}