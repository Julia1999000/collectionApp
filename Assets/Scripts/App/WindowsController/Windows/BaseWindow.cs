using UnityEngine;

public class BaseWindow : MonoBehaviour {

    public void Hide() {
        gameObject.SetActive(false);
        IWindowsController.Instance.CloseWindow();
    }

    public void Show() {
        gameObject.SetActive(true);
        IWindowsController.Instance.OpenWindow(this);
    }
    
}