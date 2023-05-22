using UnityEngine;


namespace App.WindowSystem.Windows {
    
    public class WindowOnsetType {
        public static Vector3 ONSET_FROM_BOTTOM = new Vector3(0, -1000, 0);
        public static Vector3 ONSET_FROM_TOP = new Vector3(0, 1000, 0);
        public static Vector3 ONSET_FROM_LEFT = new Vector3(-900, 0, 0);
        public static Vector3 ONSET_FROM_RIGHT = new Vector3(900, 0, 0);
        public static Vector3 ONSET_FROM_CENTER = new Vector3(0, 0, 0);
        
        public static Vector3 NORMAL_SCALE_ONSET = Vector3.one;
        public static Vector3 SCALE_ONSET_08 = new Vector3(0.8f, 0.8f, 0.8f);
    }
    
}