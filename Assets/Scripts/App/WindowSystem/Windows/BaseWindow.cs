using App.Main;
using UnityEngine;
using UnityEngine.UI;


namespace App.WindowSystem.Windows {

    public class BaseWindow : MonoBehaviour {

        [SerializeField] private Button _closeWindowBtn;
        [SerializeField] private WindowsType _windowsType = WindowsType.DEFAULT;

        private float WINDOW_ANIMATION_TIME = 0.5f;
        
        public WindowsType WindowType {
            get => _windowsType;
            set => _windowsType = value;
        }

        private void Start() {
            _closeWindowBtn.onClick.AddListener(() => IMainController.Instance.WindowsController.CloseWindow(gameObject));
        }
        
    }
    
}
