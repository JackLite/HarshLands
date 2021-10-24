using UnityEngine;
using UnityEngine.UI;

namespace Main.Player.Stamina
{
    public class StaminaMono : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        public void UpdateStamina(float val)
        {
            slider.value = val;
        }

        public void SetMax(float max)
        {
            slider.maxValue = max;
        }
    }
}