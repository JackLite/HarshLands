using TMPro;
using UnityEngine;

namespace Common.GUI
{
    /// <summary>
    /// Общий класс для кнопок с текстом
    /// </summary>
    public class ButtonWithText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI btnText;

        /// <summary>
        /// Установить текст на кнопке
        /// </summary>
        /// <param name="text">Текст, который отобразится на кнопке</param>
        public void SetText(string text)
        {
            btnText.text = text;
        }
    }
}