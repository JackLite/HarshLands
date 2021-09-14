using System;
using Common.GUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utils.UnityExtensions;

namespace InteractiveObjects.GUI
{
    /// <summary>
    /// Панель с кнопками взаимодействия с предметами
    /// Живёт в World Space
    /// </summary>
    public class ActionsPanelUI : MonoBehaviour
    {
        [SerializeField]
        private Transform buttonsParent;

        public void Awake()
        {
            var camera = Camera.main;
            transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, 0, 0);
            GetComponent<Canvas>().worldCamera = camera;
        }

        public void ShowAtObject(Transform target)
        {
            var position = target.position;
            transform.position = position + Vector3.up * 3;
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void AddButton(string text, Action onClick)
        {
            Addressables.InstantiateAsync("ActionButton", buttonsParent).Completed 
                += handler => OnButtonCreated(handler, text, onClick);
        }

        private void OnButtonCreated(AsyncOperationHandle<GameObject> handler, string text, Action onClick)
        {
            if (handler.Result == null)
                throw handler.OperationException;
            
            handler.Result.GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke());
            handler.Result.GetComponent<ButtonWithText>().SetText(text);
        }

        public void ResetButtons()
        {
            buttonsParent.RemoveChildren();
        }
    }
}