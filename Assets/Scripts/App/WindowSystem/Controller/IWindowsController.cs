using App.WindowSystem.Windows;
using UnityEngine;

namespace App.WindowSystem.Controller {

    public class IWindowsController : MonoBehaviour {

        public Transform Transform { get; set; }
        
        public GameObject Fade { get; set; }

        public void OpenWindow(WindowsType windowType, Vector3 windowOnsetPosition) { }

        public void CloseWindow(GameObject window) { }

    }

}