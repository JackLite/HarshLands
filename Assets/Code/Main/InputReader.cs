using UnityEngine;
using UnityEngine.InputSystem;

namespace Main
{
    public class InputReader : MonoBehaviour
    {
        private Vector3 _force;
        public static Vector3 Force;
        
        public void ProcessInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            Force = new Vector3(value.x, 0, value.y);
        }
    }
}
