using UnityEngine;


namespace App.WindowSystem.Windows {

    public class SettingsWindow : BaseWindow {

        [SerializeField] private static WindowsType _windowType;

        public WindowsType WindowType {
            get => _windowType;
            set => _windowType = value;
        }

        private void Awake() {
            Init();
        }

        private void Init() {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            
            if (transform.position == WindowOnsetType.ONSET_FROM_CENTER) {
                transform.localScale = WindowOnsetType.SCALE_ONSET_08;
            }
        }

    }
    
}