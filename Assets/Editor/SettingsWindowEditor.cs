using App.WindowSystem.Windows;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SettingsWindow))]
public class SettingsWindowEditor : Editor {
    
    private string windowTypeLabel = "Window Type";
   
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        SettingsWindow window = (SettingsWindow) target;
        window.WindowType = (WindowsType)EditorGUILayout.EnumPopup(windowTypeLabel, window.WindowType);
    }

}
