using UnityEngine;


namespace App.WindowSystem.Windows {

    
    public enum WindowOnsetType {
        ONSET_FROM_BOTTOM,
        ONSET_FROM_TOP,
        ONSET_FROM_LEFT,
        ONSET_FROM_RIGHT,
        ONSET_FROM_CENTER
    }

    
    public class WindowScale {
        public static Vector3 NORMAL_SCALE_ONSET = Vector3.one;
        public static Vector3 SCALE_ONSET_08 = new Vector3(0.8f, 0.8f, 0.8f);
    }

    
    public static class WindowsOnsetTypeMapper {
        public static Vector3 GetLegalPosition(WindowOnsetType legalEntityType, WindowsType windowsType) {
            var windowTypeStr = WindowsTypeMapper.GetReadableString(windowsType);
            var pathToPrefab = AppPath.PATH_TO_PREFABS_WINDOWS + windowTypeStr;
            var prefab = Resources.Load<GameObject>(pathToPrefab);

            var pivotX = prefab.GetComponent<RectTransform>().pivot.x;
            var pivotY = prefab.GetComponent<RectTransform>().pivot.y;
            var width = prefab.GetComponent<RectTransform>().rect.width;
            var height = prefab.GetComponent<RectTransform>().rect.height;
            var posX = Screen.width / 2.0f + width * pivotX;
            var posY = Screen.height / 2.0f + height * pivotY;
            
            switch (legalEntityType) {
                case WindowOnsetType.ONSET_FROM_BOTTOM : return new Vector3(0, -posY, 0);
                case WindowOnsetType.ONSET_FROM_TOP : return new Vector3(0, posY, 0);
                case WindowOnsetType.ONSET_FROM_LEFT : return new Vector3(-posX, 0, 0);
                case WindowOnsetType.ONSET_FROM_RIGHT : return new Vector3(posX, 0, 0);
                default : return new Vector3(0, 0, 0);
            }
        }
    }
    
}