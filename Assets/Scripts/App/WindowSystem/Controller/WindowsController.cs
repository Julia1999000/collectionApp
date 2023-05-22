using System.Collections.Generic;
using App.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UI;


namespace App.WindowSystem.Controller {
    
    public class WindowsController : IWindowsController {
        
        private static string PATH_TO_PREFABS_WINDOWS = "Prefabs/Windows";
        
        private float ANIMATION_TIME = 0.5f;
        private float HIDDEN_FADE_ALPHA = 0f;
        private float SHOWN_FADE_ALPHA = 0.7f;
        private Transform _windowsLayerTransform;
        private GameObject _fade;
        private readonly Dictionary<GameObject, Vector3> _openWindows = new Dictionary<GameObject, Vector3>();
        private GameObject _currentWindow;
        private Vector3 _currentWindOnsetPos;

        public Transform Transform {
            get => _windowsLayerTransform;
            set => _windowsLayerTransform = value;
        }

        public GameObject Fade {
            get => _fade;
            set => _fade = value;
        }

        public void OpenWindow(WindowsType windowType, Vector3 windowOnsetPosition) {
            _currentWindow = LoadWindow(WindowsTypeMapper.GetReadableString(windowType), windowOnsetPosition);
            _currentWindOnsetPos = windowOnsetPosition;
            _openWindows.Add(_currentWindow, windowOnsetPosition);
            ShowFade();
            OpenWindowAnimation();
        }

        public void CloseWindow(GameObject window) {
            _currentWindow = window;
            _currentWindOnsetPos = _openWindows[window];
            CloseWindowAnimation();
            HideFade();
        }

        private GameObject LoadWindow(string windowType, Vector3 position) {
            var pathToPrefab = PATH_TO_PREFABS_WINDOWS + windowType;
            var prefab = Resources.Load<GameObject>(pathToPrefab);
            var window = Instantiate(prefab, position, Quaternion.identity, _windowsLayerTransform);
            return window;
        }

        private void ShowFade() {
            _fade.GetComponent<Image>().enabled = true;
            FadeAnimation(true);
        }
        
        private void HideFade() {
            FadeAnimation(false);
        }

        private void FadeAnimation(bool isShow) {
            LeanTween.cancel(_fade);
            var toAlpha = isShow ? SHOWN_FADE_ALPHA : HIDDEN_FADE_ALPHA;
            LeanTween.alphaCanvas(_fade.GetComponent<CanvasGroup>(), toAlpha, ANIMATION_TIME)
                .setOnComplete(() => {
                    if (!isShow) {
                        _fade.GetComponent<Image>().enabled = false;
                    }
                });
        }
        
        private void OpenWindowAnimation() {
            DefineAnimation(true);
        }

        private void CloseWindowAnimation() {
            DefineAnimation(false);
        }

        private void DefineAnimation(bool isShow) {
            var toAlpha = isShow ? 1 : 0;
            LeanTween.cancel(_currentWindow);
            LeanTween.alphaCanvas(_currentWindow.GetComponent<CanvasGroup>(), toAlpha, ANIMATION_TIME);
            
            if (_currentWindOnsetPos == WindowOnsetType.ONSET_FROM_CENTER) {
                OnsetFromCenterAnimation(isShow);
                return;
            }
            if (_currentWindOnsetPos == WindowOnsetType.ONSET_FROM_BOTTOM || _currentWindOnsetPos == WindowOnsetType.ONSET_FROM_TOP) {
                VerticalOnsetAnimation(isShow);
                return;
            }
            if (_currentWindOnsetPos == WindowOnsetType.ONSET_FROM_LEFT || _currentWindOnsetPos == WindowOnsetType.ONSET_FROM_RIGHT) {
                HorizontalOnsetAnimation(isShow);
                return;
            }
        }
        
        private void HorizontalOnsetAnimation(bool isShow) {
            var toX = isShow ? 0f : _currentWindOnsetPos.x;
            LeanTween.moveX(_currentWindow.GetComponent<RectTransform>(), toX, ANIMATION_TIME).setEaseSpring()
                .setOnComplete(() => {
                    if (!isShow) {
                        DeleteWindow();
                    }
                });
        }
        
        private void VerticalOnsetAnimation(bool isShow) {
            var toY = isShow ? 0f : _currentWindOnsetPos.y;
            LeanTween.moveY(_currentWindow.GetComponent<RectTransform>(), toY, ANIMATION_TIME).setEaseSpring()
                .setOnComplete(() => {
                    if (!isShow) {
                        DeleteWindow();
                    }
                });
        }
        
        private void OnsetFromCenterAnimation(bool isShow) {
            var toScale = isShow ? WindowOnsetType.NORMAL_SCALE_ONSET : WindowOnsetType.SCALE_ONSET_08;
            LeanTween.scale(_currentWindow, toScale, ANIMATION_TIME).setEaseSpring().setOnComplete(() => {
                if (!isShow) {
                    DeleteWindow();
                }
            });
        }

        private void DeleteWindow() {
            _openWindows.Remove(_currentWindow);
            Destroy(_currentWindow);
        }

        
    }
    
}