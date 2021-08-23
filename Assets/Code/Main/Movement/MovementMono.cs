using UnityEngine;

namespace Main.Movement
{
    public class MovementMono : MonoBehaviour
    {
        [SerializeField]
        private float speed = 75f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 force)
        {
            _rigidbody.AddForce(force * speed, ForceMode.Acceleration);
        }
    }
}