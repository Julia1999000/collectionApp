using UnityEngine;


namespace App.WindowSystem.Windows {

    public class SettingsWindow : BaseWindow {
        
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