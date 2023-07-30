using System;
using UnityEngine;

namespace GestureController {
    
    public class SwipeController : MonoBehaviour {

        private static event OnSwipeInput _swipeEvent;
        public delegate void OnSwipeInput(Vector2 direction);

        private float SWIPE_DISTANCE = 80;
        private Vector2 tapPosition; 
        private Vector2 swipeDelta;
        private bool isSwiping;
        private bool isMobile;

        public static OnSwipeInput SwipeEvent {
            get => _swipeEvent;
            set => _swipeEvent = value;
        }

        private void Start() {
            isMobile = Application.isMobilePlatform;
        }

        private void Update() {
            if (isMobile) {
                FixTouchOnMobile();
            } else {
                FixTouchOnPC();
            }

            CheckSwipe();
        }

        private void FixTouchOnMobile() {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                } else if (Input.GetTouch(0).phase == TouchPhase.Canceled 
                           || Input.GetTouch(0).phase == TouchPhase.Ended) {
                    ResetSwipe();
                }
            }
        }

        private void FixTouchOnPC() {
            if (Input.GetMouseButtonDown(0)) {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            } else if (Input.GetMouseButtonUp(0)) {
                ResetSwipe();
            }
        }

        private void CheckSwipe() {
            swipeDelta = Vector2.zero;

            if (isSwiping) {
                if (!isMobile && Input.GetMouseButton(0)) {
                    swipeDelta = (Vector2)Input.mousePosition - tapPosition;
                } else if (Input.touchCount > 0) {
                    swipeDelta = Input.GetTouch(0).position - tapPosition;
                }
            }

            if (swipeDelta.magnitude > SWIPE_DISTANCE) {
                if (_swipeEvent != null) {
                    if (Math.Abs(swipeDelta.x) > Math.Abs(swipeDelta.y)) {
                        _swipeEvent(swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                    } else {
                        _swipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                    }
                }
                ResetSwipe();
            }
        }
        
        private void ResetSwipe() {
            isSwiping = false;
            tapPosition = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
        
        
    }

}