using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NavigationBar {
    
    public class INavigationBarController : MonoBehaviour{
        
        public GameObject ScreenContainer { get; set; }
        
        public Dictionary<Button, Transform> BunchOfBtnsAndScreens { get; set; }
        
    }
    
}