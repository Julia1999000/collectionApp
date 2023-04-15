using UnityEngine;

public class IMainController : MonoBehaviour {
    
    public static MainController Instance {  get; protected set; }

    public Camera Camera { get; set; }
    
    public ScenesNavigator ScenesNavigator { get; }
    
}
