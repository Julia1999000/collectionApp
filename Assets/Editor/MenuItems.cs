using App;
using Navigation;
using UnityEditor;
using UnityEditor.SceneManagement;


public class MenuItems {


    

    [MenuItem("Scenes/Init")]
    private static void NewMenuOptionInitScene() {
        OpenScene(AppPath.PATH_TO_SCENES + SceneNames.EXPANDED_INIT);
    }

    [MenuItem("Scenes/MainMenu")]
    private static void NewMenuOptionMainMenuScene() {
        OpenScene(AppPath.PATH_TO_SCENES +  SceneNames.EXPANDED_MAIN_MENU);
    }
    
    private static void OpenScene(string path) {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
    }
    
}
