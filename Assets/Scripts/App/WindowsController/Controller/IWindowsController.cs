using UnityEngine;

public class IWindowsController : MonoBehaviour {
    
    public static WindowsController Instance {  get; protected set; }

    public void OpenWindow(BaseWindow window) {}
    
    public void CloseWindow() {}
    
    public void DeleteActiveLayer() {}
    
    public void SetActiveLayer(int layer) {}
}
