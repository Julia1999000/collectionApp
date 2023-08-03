using App.WindowSystem.Windows;
using UnityEngine;

namespace App.WindowSystem.Controller {

    public class IWindowsController : MonoBehaviour {

        public Transform Transform { get; set; }
        
        public GameObject Fade { get; set; }
        
        public int CountOpenWindows { get; }

        public void OpenWindow(WindowsType windowType, WindowOnsetType windowOnsetType) { }

        public void CloseWindow(GameObject window) { }

    }

}