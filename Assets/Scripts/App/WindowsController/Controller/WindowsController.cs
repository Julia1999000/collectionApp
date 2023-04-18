using System.Collections.Generic;

public class WindowsController : IWindowsController {
    
    private static WindowsController _instance;
    private readonly Stack<int> _layerStack = new Stack<int>();
    private readonly Stack<BaseWindow> _openWindows = new Stack<BaseWindow>();
    
    public static WindowsController Instance => _instance;

    public void OpenWindow(BaseWindow window) {
        _openWindows.Push(window);
        SetActiveLayer(window.gameObject.layer);
    }

    public void CloseWindow() {
        _openWindows.Pop();
        DeleteActiveLayer();
    }

    public void SetActiveLayer(int layer) {
        _layerStack.Push(layer);
    }

    public void DeleteActiveLayer() {
        _layerStack.Pop();
    }
    
    private void Awake() {
        Init();
    }
    
    private void Init() {
        DontDestroyOnLoad(this);
        
        _instance = (_instance == null) ? this : null;
        IWindowsController.Instance = _instance;
        
        SetActiveLayer(gameObject.layer);
    }
    
}
