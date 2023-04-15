using UnityEngine;

public class MainMenu : MonoBehaviour {
    
    [SerializeField] private Camera _camera;
    
    private void Awake() {
        IMainController.Instance.Camera = _camera;
    }
    
}
