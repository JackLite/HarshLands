using UnityEngine;

namespace Utils.UI
{
    public static class CanvasContainer
    {
        private static GameObject _canvas;

        public static GameObject GetCanvas()
        {
            if (!_canvas)
                _canvas = GameObject.FindGameObjectWithTag("MainCanvas");

            return _canvas;
        }
    }
}