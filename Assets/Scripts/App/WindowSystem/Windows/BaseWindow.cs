using App.Main;
using UnityEngine;
using UnityEngine.UI;

namespace App.WindowSystem.Windows {
    
    public class BaseWindow : MonoBehaviour {

        [SerializeField] private Button _closeWindowBtn;

        private void Start() {
            _closeWindowBtn.onClick.AddListener(() => IMainController.Instance.WindowsController.CloseWindow(gameObject));
        }

    }
}