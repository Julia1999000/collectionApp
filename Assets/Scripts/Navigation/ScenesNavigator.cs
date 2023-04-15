using UnityEngine.SceneManagement;

public class ScenesNavigator : IScenesNavigator {
    
    private static string MAIN_MENU = "MainMenu";

    public override void OpenSceneMenu() {
        SceneManager.LoadScene(MAIN_MENU);
    }

}