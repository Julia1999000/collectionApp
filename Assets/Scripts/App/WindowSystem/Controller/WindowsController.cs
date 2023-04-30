using System.Collections.Generic;
using App.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace App.WindowSystem.Controller {
    
    public class WindowsController : IWindowsController {

        [SerializeField] private GameObject _settingsWindowGameObject;

        private Transform _windowsLayerTransform;
        private readonly List<GameObject> _openWindows = new List<GameObject>();
        private Color _fadeColor = new Color(Color.black.r, Color.black.g, Color.black.b, 0.5f);

        public Transform Transform {
            get => _windowsLayerTransform;
            set => _windowsLayerTransform = value;
        }

        public void OpenWindow(WindowType windowType) {
            _windowsLayerTransform.gameObject.AddComponent<Image>().color = _fadeColor;

            switch (windowType) {
                case WindowType.SETTINGS:
                    openSettingsWindow();
                    break;
            }
        }

        public void CloseWindow(GameObject window) {
            _openWindows.Remove(window);
            Destroy(window);
            Destroy(_windowsLayerTransform.gameObject.GetComponent<Image>());
        }

        private void openSettingsWindow() {
            _openWindows.Add(Instantiate(_settingsWindowGameObject, _windowsLayerTransform));
        }

    }
    
}