using UnityEngine;

public static class SpecificExtensions {
        
    public static void SetActiveLayer(GameObject gameObject) {
        IWindowsController.Instance.SetActiveLayer(gameObject.layer);
    }
    
}