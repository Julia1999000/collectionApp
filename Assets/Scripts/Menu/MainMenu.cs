using App.Main;
using App.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {

    public class MainMenu : MonoBehaviour {

        [SerializeField] private Camera _camera;
        [SerializeField] private Button _openWindowbBtn;
        [SerializeField] private Transform _windowsLayer;

        private void Start() {
            _openWindowbBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowType.SETTINGS));
        }

        private void Awake() {
            IMainController.Instance.Camera = _camera;
            IMainController.Instance.WindowsController.Transform = _windowsLayer;
        }

    }

}