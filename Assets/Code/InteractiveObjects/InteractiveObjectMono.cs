using System;
using UnityEngine;

namespace InteractiveObjects
{
    [RequireComponent(typeof(SphereCollider))]
    public class InteractiveObjectMono : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer;

        private Color _defaultColor;
        
        public event Action OnPlayerEnter;
        public event Action OnPlayerExit;

        private void Awake()
        {
            _defaultColor = _renderer.material.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnPlayerExit?.Invoke();
        }

        public void SetInteraction(bool state)
        {
            _renderer.material.color = state ? Color.red : _defaultColor;
        }
    }
}