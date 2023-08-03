using App.Main;
using UnityEngine;
using UnityEngine.UI;


namespace App.WindowSystem.Windows {

    public class BaseWindow : MonoBehaviour {

        [SerializeField] private Button _closeWindowBtn;
        [SerializeField] private WindowsType _windowsType = WindowsType.DEFAULT;
        private WindowOnsetType _onsetType = WindowOnsetType.ONSET_FROM_CENTER;

        public WindowsType WindowType {
            get => _windowsType;
            set => _windowsType = value;
        }

        public WindowOnsetType OnsetType {
            get => _onsetType;
            set => _onsetType = value;
        }

        private void Start() {
            _closeWindowBtn.onClick.AddListener(() => IMainController.Instance.WindowsController.CloseWindow(gameObject));
        }

        protected void Awake() {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            
            if (OnsetType == WindowOnsetType.ONSET_FROM_CENTER) {
                transform.localScale = WindowScale.SCALE_ONSET_08;
            }
        }
    }
    
}
