using UnityEngine;

namespace Main.Movement
{
    public struct MovementComponent
    {
        public Transform Transform;
        public MovementMono MovementMono;
        public Vector2 Translation;
        public Quaternion Rotation;
    }
}