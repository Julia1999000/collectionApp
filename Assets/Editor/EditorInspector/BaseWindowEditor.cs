using App.WindowSystem.Windows;
using UnityEditor;


[CustomEditor(typeof(BaseWindow))]
public class BaseWindowEditor : UnityEditor.Editor {
    
    private const string WINDOW_TYPE_LABEL = "Window Type";

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        BaseWindow window = (BaseWindow) target;
        var type = (WindowsType)EditorGUILayout.EnumPopup(WINDOW_TYPE_LABEL, window.WindowType);
        if (type != window.WindowType) {
            window.WindowType = type;
            EditorUtility.SetDirty(window);
        }
    }

}
