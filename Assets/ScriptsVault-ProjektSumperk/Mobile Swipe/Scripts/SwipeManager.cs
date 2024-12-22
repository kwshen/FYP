using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProjektSumperk
{
    public class SwipeManager : MonoBehaviour
    {
        [Tooltip("Min swipe distance (inches) to register as swipe")]
        [SerializeField] float minSwipeLength = 0.5f;

        [Tooltip("If true, a swipe is counted when the min swipe length is reached. If false, a swipe is counted when the touch/click ends.")]
        [SerializeField] bool triggerSwipeAtMinLength = false;

        [Tooltip("Whether to detect eight or four cardinal directions")]
        [SerializeField] bool useEightDirections = false;

        private const float EightDirAngle = 0.906f;
        private const float FourDirAngle = 0.5f;
        private const float DefaultDPI = 72f;
        private const float DpcmFactor = 2.54f;

        private static Dictionary<Swipe, Vector2> cardinalDirections = new Dictionary<Swipe, Vector2>()
    {
        { Swipe.Up, Vector2.up },
        { Swipe.Down, Vector2.down },
        { Swipe.Right, Vector2.right },
        { Swipe.Left, Vector2.left },
        { Swipe.UpRight, new Vector2(1, 1) },
        { Swipe.UpLeft, new Vector2(-1, 1) },
        { Swipe.DownRight, new Vector2(1, -1) },
        { Swipe.DownLeft, new Vector2(-1, -1) }
    };

        public delegate void OnSwipeDetectedHandler(Swipe swipeDirection, Vector2 swipeVelocity);

        private static OnSwipeDetectedHandler _onSwipeDetected;
        public static event OnSwipeDetectedHandler OnSwipeDetected
        {
            add
            {
                _onSwipeDetected += value;
                autoDetectSwipes = true;
            }
            remove
            {
                _onSwipeDetected -= value;
            }
        }

        private static Vector2 swipeVelocity;

        private static float dpcm;
        private static float swipeStartTime;
        private static float swipeEndTime;
        private static bool autoDetectSwipes;
        private static bool swipeEnded;
        private static Swipe swipeDirection;
        private static Vector2 firstPressPos;
        private static Vector2 secondPressPos;
        private static SwipeManager instance;

        private void Awake()
        {
            instance = this;
            float dpi = (Screen.dpi == 0) ? DefaultDPI : Screen.dpi;
            dpcm = dpi / DpcmFactor;
        }

        private void Update()
        {
            if (autoDetectSwipes)
            {
                DetectSwipe();
            }
        }

        private static void DetectSwipe()
        {
            if (GetTouchInput() || GetMouseInput())
            {
                if (swipeEnded)
                {
                    return;
                }

                Vector2 currentSwipe = secondPressPos - firstPressPos;
                float swipeCm = currentSwipe.magnitude / dpcm;

                if (swipeCm < instance.minSwipeLength)
                {
                    if (!instance.triggerSwipeAtMinLength)
                    {
                        swipeDirection = Swipe.Tap;
                        if (_onSwipeDetected != null)
                        {
                            _onSwipeDetected(swipeDirection, swipeVelocity);
                        }
                    }
                }

                swipeEndTime = Time.time;
                swipeVelocity = currentSwipe * (swipeEndTime - swipeStartTime);
                swipeDirection = GetSwipeDirByTouch(currentSwipe);
                swipeEnded = true;

                if (_onSwipeDetected != null)
                {
                    _onSwipeDetected(swipeDirection, swipeVelocity);
                }
            }
            else
            {
                swipeDirection = Swipe.None;
            }
        }

        public static bool IsSwipingRight() { return IsSwipingDirection(Swipe.Right); }
        public static bool IsSwipingLeft() { return IsSwipingDirection(Swipe.Left); }
        public static bool IsSwipingUp() { return IsSwipingDirection(Swipe.Up); }
        public static bool IsSwipingDown() { return IsSwipingDirection(Swipe.Down); }
        public static bool IsSwipingDownLeft() { return IsSwipingDirection(Swipe.DownLeft); }
        public static bool IsSwipingDownRight() { return IsSwipingDirection(Swipe.DownRight); }
        public static bool IsSwipingUpLeft() { return IsSwipingDirection(Swipe.UpLeft); }
        public static bool IsSwipingUpRight() { return IsSwipingDirection(Swipe.UpRight); }
        public static bool IsTapping() { return IsSwipingDirection(Swipe.Tap); }

        #region Helper Functions

        private static bool GetTouchInput()
        {
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = t.position;
                    swipeStartTime = Time.time;
                    swipeEnded = false;
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = t.position;
                    return true;
                }
                else
                {
                    if (instance.triggerSwipeAtMinLength)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool GetMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = (Vector2)Input.mousePosition;
                swipeStartTime = Time.time;
                swipeEnded = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                secondPressPos = (Vector2)Input.mousePosition;
                return true;
            }
            else
            {
                if (instance.triggerSwipeAtMinLength)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsDirection(Vector2 direction, Vector2 cardinalDirection)
        {
            var angle = instance.useEightDirections ? EightDirAngle : FourDirAngle;
            return Vector2.Dot(direction, cardinalDirection) > angle;
        }

        private static Swipe GetSwipeDirByTouch(Vector2 currentSwipe)
        {
            currentSwipe.Normalize();
            var swipeDir = cardinalDirections.FirstOrDefault(dir => IsDirection(currentSwipe, dir.Value));
            return swipeDir.Key;
        }

        private static bool IsSwipingDirection(Swipe swipeDir)
        {
            DetectSwipe();
            return swipeDirection == swipeDir;
        }

        #endregion
    }

    public enum Swipe
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
        Tap
    }
}