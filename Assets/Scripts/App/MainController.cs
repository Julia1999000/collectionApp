using UnityEngine;

public class MainController : IMainController {
    
    [SerializeField] private ScenesNavigator _scenesNavigator;
    
    private static MainController _instance;
    private Camera _camera;
    
    public static MainController Instance => _instance;
    public ScenesNavigator ScenesNavigator => _scenesNavigator;
    
    public Camera Camera {
        get => _camera;
        set => _camera = value;
    }

    private void Awake() {
        Init();
        
        _scenesNavigator.OpenSceneMenu();
    }

    private void Init() {
        DontDestroyOnLoad(this);
        
        _instance = (_instance == null) ? this : null;
        IMainController.Instance = _instance;
    }

}