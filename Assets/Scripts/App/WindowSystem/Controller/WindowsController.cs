using System.Collections.Generic;
using App.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.UI;


namespace App.WindowSystem.Controller {
    
    public class WindowsController : IWindowsController {
        
        private float ANIMATION_TIME = 0.5f;
        private float HIDDEN_FADE_ALPHA = 0f;
        private float SHOWN_FADE_ALPHA = 0.7f;
        private Transform _windowsLayerTransform;
        private GameObject _fade;
        private readonly List<GameObject> _openWindows = new List<GameObject>();
        private GameObject _currentWindow;

        public int CountOpenWindows {
            get => _openWindows.Count;
        }

        public Transform Transform {
            get => _windowsLayerTransform;
            set => _windowsLayerTransform = value;
        }

        public GameObject Fade {
            get => _fade;
            set => _fade = value;
        }

        public void OpenWindow(WindowsType windowType, WindowOnsetType windowOnsetType) {
            var pos = WindowsOnsetTypeMapper.GetLegalPosition(windowOnsetType, windowType);
            _currentWindow = LoadWindow(WindowsTypeMapper.GetReadableString(windowType), pos);
            _currentWindow.GetComponent<BaseWindow>().OnsetType = windowOnsetType;
            _openWindows.Add(_currentWindow);
            ShowFade();
            OpenWindowAnimation();
        }

        public void CloseWindow(GameObject window) {
            _currentWindow = window;
            CloseWindowAnimation();
            HideFade();
        }

        private GameObject LoadWindow(string windowType, Vector3 position) {
            var pathToPrefab = AppPath.PATH_TO_PREFABS_WINDOWS + windowType;
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

            var onsetType = _currentWindow.GetComponent<BaseWindow>().OnsetType;
            if (onsetType == WindowOnsetType.ONSET_FROM_CENTER) {
                OnsetFromCenterAnimation(isShow);
                return;
            }
            if (onsetType == WindowOnsetType.ONSET_FROM_BOTTOM || onsetType == WindowOnsetType.ONSET_FROM_TOP) {
                VerticalOnsetAnimation(isShow);
                return;
            }
            if (onsetType == WindowOnsetType.ONSET_FROM_LEFT || onsetType == WindowOnsetType.ONSET_FROM_RIGHT) {
                HorizontalOnsetAnimation(isShow);
                return;
            }
        }
        
        private void HorizontalOnsetAnimation(bool isShow) {
            var toX = isShow ? 0f : WindowsOnsetTypeMapper.GetLegalPosition(_currentWindow.GetComponent<BaseWindow>().OnsetType, 
                _currentWindow.GetComponent<BaseWindow>().WindowType).x;
            LeanTween.moveX(_currentWindow.GetComponent<RectTransform>(), toX, ANIMATION_TIME).setEaseSpring()
                .setOnComplete(() => {
                    if (!isShow) {
                        DeleteWindow();
                    }
                });
        }
        
        private void VerticalOnsetAnimation(bool isShow) {
            var toY = isShow ? 0f : WindowsOnsetTypeMapper.GetLegalPosition(_currentWindow.GetComponent<BaseWindow>().OnsetType, 
                _currentWindow.GetComponent<BaseWindow>().WindowType).y;
            LeanTween.moveY(_currentWindow.GetComponent<RectTransform>(), toY, ANIMATION_TIME).setEaseSpring()
                .setOnComplete(() => {
                    if (!isShow) {
                        DeleteWindow();
                    }
                });
        }
        
        private void OnsetFromCenterAnimation(bool isShow) {
            var toScale = isShow ? WindowScale.NORMAL_SCALE_ONSET : WindowScale.SCALE_ONSET_08;
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