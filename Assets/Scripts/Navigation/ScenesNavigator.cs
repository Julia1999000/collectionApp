using UnityEngine.SceneManagement;

namespace  Navigation {
    
    public class ScenesNavigator : IScenesNavigator {
        
        public override void OpenSceneMenu() {
            SceneManager.LoadScene(SceneNames.MAIN_MENU);
        }

    }
    
}