using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelEditor {
    
    public class LevelEditorWindow : EditorWindow {

        private const string FORMAT_PATH_TO_LEVEL = @"Assets/Resources/Data/LevelsData/{0}.asset";
        private const string PATH_TO_LEVELS_DATA = "Assets/Resources/Data/LevelsData/";
        private const string PATH_TO_STYLES = "Styles/";
        private const string SELECTED_LEVEL_SKIN = "LevelEditorSelectedLevelSkin";
        private const string SELECTED_LEVEL_STYLE = "button";
        private const string WINDOW_TITLE = "Level Editor";
        private const string LEVELS_HEADER = "Levels:";
        private const string SETTINGS_HEADER = "Settings:";
        private const string ADD_BTN_TXT = "Add Level";
        private const string SORT_BTN_TXT = "Sort Levels";
        private const string REMOVE_BTN_TXT = "Remove Level";
        private const string LEVEL_NAME_LABEL = "Level Name";
        private const string LEVEL_NUMBER_LABEL = "Level Number";
        private const string COUNT_CELLS_VERTICALLY_LABEL = "Count Cells Vertically";
        private const string COUNT_CELLS_HORIZONTALLY_LABEL = "Count Cells Horizontally";
        
        private const int MIN_WINDOW_HEIGHT = 500 + PADDING * 2;
        private const int MIN_WINDOW_WIDTH = 600 + PADDING * 3;
        private const int PADDING = 5;
        private const int BUTTON_HEIGHT = 20;
        
        List<LevelData> _levels = new List<LevelData>();
        private Vector2 _scrollPosition;
        private LevelData _selectedLevel;
        private GUIStyle _levelNameStyle;
        private GUIStyle _levelItemStyle;
        private GUIStyle _selectedLevelStyle;
        private Texture2D _sectionTexture;
        private Rect _levelsSection;
        private Rect _levelSettingsSection;
        private Color _sectionColor = new Color(70f / 255f, 70f / 255f, 70f / 255f, 1f);

        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow() {
            var window = GetWindow(typeof(LevelEditorWindow), false, WINDOW_TITLE);
            window.minSize = new Vector2(MIN_WINDOW_WIDTH, MIN_WINDOW_HEIGHT);
            window.Show();
        }

        private void OnEnable() {
            GetLevelsData();
            InitTextures();
        }

        private void OnGUI () {
            InitStyles();
            DrawLayouts();
            DrawLevels();
            DrawSettings();
        }
        
        private void InitTextures() {
            _sectionTexture = new Texture2D(1, 1);
            _sectionTexture.SetPixel(0, 0, _sectionColor);
            _sectionTexture.Apply();
        }

        private void InitStyles() {
            var selectedLevelSkin = Resources.Load<GUISkin>(PATH_TO_STYLES + SELECTED_LEVEL_SKIN);
            _selectedLevelStyle = selectedLevelSkin.GetStyle(SELECTED_LEVEL_STYLE);
            _levelItemStyle = new GUIStyle(GUI.skin.button);
            
            _levelNameStyle = new GUIStyle(GUI.skin.label);
            _levelNameStyle.fontStyle = FontStyle.Bold;
        }
        
        private void DrawLayouts() {
            var widthOfSections = position.width - 3 * PADDING;
            var heightOfSections = position.height - 2 * PADDING;

            _levelsSection = new Rect(PADDING, PADDING, widthOfSections / 3, heightOfSections);
            _levelSettingsSection = new Rect(_levelsSection.width + 2 * PADDING, PADDING, widthOfSections * 2 / 3, heightOfSections);
            
            GUI.DrawTexture(_levelsSection, _sectionTexture);
            GUI.DrawTexture(_levelSettingsSection, _sectionTexture);
        }

        private void DrawLevels() {
            var scrollViewHeight = _levelsSection.height - 4 * PADDING - BUTTON_HEIGHT;
            var scrollViewWidth = _levelsSection.width - 2 * PADDING;
            var boxContent = new Rect(PADDING, PADDING, scrollViewWidth, scrollViewHeight);
            
            GUILayout.BeginArea(_levelsSection);
            GUILayout.BeginArea(boxContent);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LEVELS_HEADER, GUILayout.Height(BUTTON_HEIGHT));
            if (GUILayout.Button(SORT_BTN_TXT,  GUILayout.Height(BUTTON_HEIGHT))) {
                OnSortButtonClicked();
            }
            GUILayout.EndHorizontal();
            
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            foreach (var levelData in _levels) {
                DrawLevelItem(levelData);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
            DrawButtons();
            GUILayout.EndArea();
        }

        private void DrawLevelItem(LevelData levelData) {
            var btnStyle = _selectedLevel != null && levelData.LevelID == _selectedLevel.LevelID ? _selectedLevelStyle : _levelItemStyle;
            var isClicked = GUILayout.Button(levelData.name, btnStyle, GUILayout.Height(BUTTON_HEIGHT));

            if (isClicked) {
                _selectedLevel = levelData;
            }
        }
        
        private void DrawSettings() {
            var deltaSize = 2 * PADDING;
            var boxContent = new Rect(PADDING, PADDING, _levelSettingsSection.width - deltaSize, _levelSettingsSection.height - deltaSize);
            
            GUILayout.BeginArea(_levelSettingsSection);
            GUILayout.BeginArea(boxContent);
            GUILayout.Label(SETTINGS_HEADER, GUILayout.Height(BUTTON_HEIGHT));
            GUILayout.Space(PADDING);
            
            if (_selectedLevel != null) {
                GUILayout.BeginHorizontal();
                GUILayout.Label(LEVEL_NAME_LABEL);
                GUILayout.Label(_selectedLevel.name, _levelNameStyle);
                GUILayout.EndHorizontal();
                
                var number = EditorGUILayout.IntField(LEVEL_NUMBER_LABEL, _selectedLevel.Number);
                if (number != 0 && CheckValidityOfNumber(number)) {
                    ChangeLevelNumber(number);
                }

                _selectedLevel.CountCellsHorizontally = EditorGUILayout.IntSlider(COUNT_CELLS_HORIZONTALLY_LABEL,
                    _selectedLevel.CountCellsHorizontally, LevelData.MIN_COUNT_CELLS, LevelData.MAX_COUNT_CELLS);
                _selectedLevel.CountCellsVertically = EditorGUILayout.IntSlider(COUNT_CELLS_VERTICALLY_LABEL, 
                    _selectedLevel.CountCellsVertically, LevelData.MIN_COUNT_CELLS, LevelData.MAX_COUNT_CELLS);

                EditorUtility.SetDirty(_selectedLevel);
            }
            
            GUILayout.EndArea();
            GUILayout.EndArea();
        }

        private bool CheckValidityOfNumber(int number) {
            var found = _levels.FindAll(it => it.Number == number);
            return !found.Any();
        }

        private void ChangeLevelNumber(int number) {
            _selectedLevel.Number = number;
            _selectedLevel.name = string.Format(LevelData.NAME, number);
                     
            string assetPath =  AssetDatabase.GetAssetPath(_selectedLevel);
            AssetDatabase.RenameAsset(assetPath, _selectedLevel.name);
            AssetDatabase.SaveAssets();
        }
        
        
        private void DrawButtons() {
            var buttonSectionX = PADDING;
            var buttonSectionY = _levelsSection.height - PADDING - BUTTON_HEIGHT;
            var buttonSection = new Rect(buttonSectionX, buttonSectionY, _levelsSection.width - 2 * PADDING, BUTTON_HEIGHT);
            
            GUILayout.BeginArea(buttonSection);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(ADD_BTN_TXT,  GUILayout.Height(BUTTON_HEIGHT))) {
                OnAddButtonClicked();
            }
            GUILayout.Space(PADDING);
            if (GUILayout.Button(REMOVE_BTN_TXT, GUILayout.Height(BUTTON_HEIGHT))) {
                OnRemoveButtonClicked();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void OnAddButtonClicked() { 
            var newLevel = ScriptableObject.CreateInstance<LevelData>();
            var number = _levels.Last().Number + 1;
            newLevel.Number = number;
            newLevel.name = string.Format(LevelData.NAME, number);
            
            AssetDatabase.CreateAsset(newLevel, string.Format(FORMAT_PATH_TO_LEVEL, newLevel.name));
            AssetDatabase.SaveAssets();
            _levels.Add(newLevel);
        }
        
        private void OnRemoveButtonClicked() {
            if (_selectedLevel != null) { 
                string path = AssetDatabase.GetAssetPath(_selectedLevel);
                AssetDatabase.DeleteAsset(path);
                _levels.Remove(_selectedLevel);
                _selectedLevel = null;
            }
        }

        private void OnSortButtonClicked() {
            _levels.Sort(delegate(LevelData x, LevelData y) {
                return x.Number.CompareTo(y.Number);
            });
        }

        private void GetLevelsData() { 
            _levels.Clear();
            var paths = AssetDatabase.FindAssets("", new[] { PATH_TO_LEVELS_DATA });
           
            foreach (var item in paths) {
                _levels.Add(AssetDatabase.LoadAssetAtPath<LevelData>(AssetDatabase.GUIDToAssetPath(item)));
            }
        }
        
    }
    
}