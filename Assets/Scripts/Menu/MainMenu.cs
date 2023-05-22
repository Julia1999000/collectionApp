using System.Collections.Generic;
using App.Main;
using App.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {

    public class MainMenu : MonoBehaviour {

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _windowsLayer;
        [SerializeField] private GameObject _fade;
        [SerializeField] private GameObject _navBarScreenContainer;
        [SerializeField] private Transform _navBarButtonContainer;
        [SerializeField] private Button _showWindOnLeftBtn;
        [SerializeField] private Button _showWindOnRightBtn;
        [SerializeField] private Button _showWindOnTopBtn;
        [SerializeField] private Button _showWindOnBottomBtn;
        [SerializeField] private Button _showWindFromCenterBtn;

        private void Start() {
            _showWindOnLeftBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowsType.SETTINGS, WindowOnsetType.ONSET_FROM_LEFT));
            _showWindOnRightBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowsType.SETTINGS, WindowOnsetType.ONSET_FROM_RIGHT));
            _showWindOnTopBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowsType.SETTINGS, WindowOnsetType.ONSET_FROM_TOP));
            _showWindOnBottomBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowsType.SETTINGS, WindowOnsetType.ONSET_FROM_BOTTOM));
            _showWindFromCenterBtn.onClick.AddListener(() => MainController.Instance.WindowsController.OpenWindow(WindowsType.SETTINGS, WindowOnsetType.ONSET_FROM_CENTER));
        }

        private void Awake() {
            IMainController.Instance.Camera = _camera;
            
            IMainController.Instance.WindowsController.Transform = _windowsLayer;
            IMainController.Instance.WindowsController.Fade = _fade;

            var bunchOfBtnsAndScreens = new Dictionary<Button, Transform>();
            var screenContainer = _navBarScreenContainer.transform.GetChild(0).transform;
            for (int i = 0; i < _navBarButtonContainer.childCount; i++) {
                bunchOfBtnsAndScreens.Add(_navBarButtonContainer.GetChild(i).gameObject.GetComponent<Button>(), screenContainer.GetChild(i));
            }

            IMainController.Instance.NavigationBarController.ScreenContainer = _navBarScreenContainer;
            IMainController.Instance.NavigationBarController.BunchOfBtnsAndScreens = bunchOfBtnsAndScreens;
        }

    }

}