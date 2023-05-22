using Navigation;
using UnityEditor;
using UnityEditor.SceneManagement;


public class MenuItems {

    private static string PATH_TO_SCENES = "Assets/Scenes/";
    

    [MenuItem("Scenes/Init")]
    private static void NewMenuOptionInitScene() {
        OpenScene(PATH_TO_SCENES + SceneNames.EXPANDED_INIT);
    }

    [MenuItem("Scenes/MainMenu")]
    private static void NewMenuOptionMainMenuScene() {
        OpenScene(PATH_TO_SCENES +  SceneNames.EXPANDED_MAIN_MENU);
    }
    
    private static void OpenScene(string path) {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
    }
    
}
