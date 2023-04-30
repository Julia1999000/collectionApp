using App.WindowSystem.Windows;
using UnityEngine;

namespace App.WindowSystem.Controller {

    public class IWindowsController : MonoBehaviour {

        public Transform Transform { get; set; }

        public void OpenWindow(WindowType windowType) { }

        public void CloseWindow(GameObject window) { }

    }

}