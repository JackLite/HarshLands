using System;
using System.Collections.Generic;
using Common.GUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace InteractiveObjects.GUI
{
    /// <summary>
    /// Панель с кнопками взаимодействия с предметами
    /// Живёт в World Space
    /// </summary>
    public class ActionsPanelUI : MonoBehaviour
    {
        private const int START_BUTTONS_COUNT = 4;
        [SerializeField]
        private Transform buttonsParent;

        private readonly List<ButtonData> _buttons = new List<ButtonData>();
        private readonly Queue<ButtonData> _availableButtons = new Queue<ButtonData>();

        public void Awake()
        {
            var camera = Camera.main;
            transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, 0, 0);
            GetComponent<Canvas>().worldCamera = camera;
            for(var i = START_BUTTONS_COUNT; i > 0; i--)
                CreateButton();
            gameObject.SetActive(false);
        }

        public void ShowAtObject(Transform target)
        {
            var position = target.position;
            transform.position = position + Vector3.up * 3;
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            ResetButtons();
            gameObject.SetActive(false);
        }
        
        public void AddButton(string text, Action onClick)
        {
            var button = _availableButtons.Dequeue();
            button.Button.onClick.AddListener(() => onClick?.Invoke());
            button.ButtonWithText.SetText(text);
            button.GameObject.SetActive(true);
        }

        private async void CreateButton()
        {
            var task = Addressables.InstantiateAsync("ActionButton", buttonsParent).Task;
            await task;

            if (task.Result)
                OnButtonCreated(task.Result);
            else
                throw new Exception("Can't create button for actions panel");
        }

        private void OnButtonCreated(GameObject result)
        {
            var buttonData = new ButtonData
            {
                GameObject = result,
                Button = result.GetComponent<Button>(),
                ButtonWithText = result.GetComponent<ButtonWithText>()
            };
            _buttons.Add(buttonData);
            _availableButtons.Enqueue(buttonData);
            result.SetActive(false);
        }

        private void ResetButtons()
        {
            _availableButtons.Clear();

            foreach (var buttonData in _buttons)
            {
                buttonData.Button.onClick.RemoveAllListeners();
                buttonData.GameObject.SetActive(false);
                _availableButtons.Enqueue(buttonData);
            }
        }

        private struct ButtonData
        {
            public Button Button;
            public ButtonWithText ButtonWithText;
            public GameObject GameObject;
        }
    }
}