using System;
using System.Collections.Generic;
using GestureController;
using UnityEngine;
using UnityEngine.UI;

namespace NavigationBar {
    
    public class NavigationBarController : INavigationBarController {

        private GameObject _screenContainer;
        private Dictionary<Button, Transform> _bunchOfBtnsAndScreens;
        private float _widthScreenContainer;
        private float _widthScreen;
        private float SCROLL_ANIMATION_TIME = 0.3f;
        private float SWIPE_ANIMATION_TIME = 0.1f;

        public GameObject ScreenContainer {
            get => _screenContainer;
            set {
                _screenContainer = value;
            }
        }

        public Dictionary<Button, Transform> BunchOfBtnsAndScreens {
            get => _bunchOfBtnsAndScreens;
            set => _bunchOfBtnsAndScreens = value;
        }

        private void Start() {
            foreach (var bunch in _bunchOfBtnsAndScreens) {
                bunch.Key.onClick.AddListener(() => OnClickedNavBar(bunch.Value));
            }

            SwipeController.SwipeEvent += OnSwipe;

            _widthScreen = _screenContainer.GetComponent<RectTransform>().rect.width;
            _widthScreenContainer = _widthScreen * _bunchOfBtnsAndScreens.Count;
        }
        
        private void OnClickedNavBar(Transform screen) {
            var sign = 0 > screen.position.x ? 1 : -1;
            var toX = _screenContainer.transform.position.x + 0 - screen.position.x;
            SwipeScreen(toX, SCROLL_ANIMATION_TIME);
        }

        private void OnSwipe(Vector2 direction) {
            if (direction == Vector2.right) {
                if (_screenContainer.transform.position.x != 0) {
                     SwipeScreen(_screenContainer.transform.position.x + _widthScreen, SWIPE_ANIMATION_TIME);
                }
            } else if (direction == Vector2.left) {
                if (_screenContainer.transform.position.x != -(_widthScreenContainer - _widthScreen)) {
                    SwipeScreen(_screenContainer.transform.position.x - _widthScreen, SWIPE_ANIMATION_TIME);
                }
            }
        }

        private void SwipeScreen(float toX, float animTime) {
            LeanTween.cancel(_screenContainer);
            LeanTween.moveX(_screenContainer.GetComponent<RectTransform>(), toX, animTime);
        }

    }
    
}