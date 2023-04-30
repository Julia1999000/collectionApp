using App.WindowSystem.Controller;
using Navigation;
using UnityEngine;

namespace App.Main {
    
    public class IMainController : MonoBehaviour {
        
        public static MainController Instance { get; protected set; }

        public Camera Camera { get; set; }

        public ScenesNavigator ScenesNavigator { get; }

        public WindowsController WindowsController { get; }
         
    }
    
}