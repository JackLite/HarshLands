using UnityEngine;

namespace Main.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementMono : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Rotate(Vector3 direction, float rotationSpeed)
        {
            var oldRotation = transform.rotation;
            var lookRotation = Quaternion.LookRotation(direction);
            var newRotation = Quaternion.RotateTowards(oldRotation, lookRotation, rotationSpeed);

            transform.rotation = newRotation;
        }

        public void Move(Vector3 direction, float speed)
        {
            _rigidbody.AddForce(direction * speed, ForceMode.Acceleration);
        }
    }
}