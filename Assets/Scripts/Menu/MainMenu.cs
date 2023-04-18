using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _button;

    private void Awake() {
        IMainController.Instance.Camera = _camera;

        SetActiveLayer();
    }

    private void SetActiveLayer() {
        SpecificExtensions.SetActiveLayer(_button.gameObject);
    }
    
}
