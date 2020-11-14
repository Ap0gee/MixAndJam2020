using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;

        private FiniteStateMachine stateMachine;

        private bool isPaused = false;

        public static bool IsPaused
        {
            get { return instance.isPaused; }
            private set { instance.isPaused = value; }
        }

        public static void PauseInput()
        {
            IsPaused = true;
        }

        public static void UnPauseInput()
        {
            IsPaused = false;
        }

        public static void TogglePauseInput()
        {
            IsPaused = !IsPaused;
        }

        public static float GetAxis(string axisName)
        {
            return IsPaused ? 0 : Input.GetAxis(axisName);
        }

        public static float GetAxisRaw(string axisName)
        {
            return IsPaused ? 0 : Input.GetAxisRaw(axisName);
        }

        public static bool GetButton(string buttonName)
        {
            return IsPaused ? false : Input.GetButton(buttonName);
        }

        public static bool GetButtonUp(string buttonName)
        {
            return IsPaused ? false : Input.GetButtonUp(buttonName);
        }

        public static bool GetButtonDown(string buttonName)
        {
            return IsPaused ? false : Input.GetButtonDown(buttonName);
        }

        public static bool GetKeyDown(KeyCode key)
        {
            return IsPaused ? false : Input.GetKeyDown(key);
        }

        public static bool GetKeyDown(string name)
        {
            return IsPaused ? false : Input.GetKeyDown(name);
        }

        public static bool GetKeyUp(string name)
        {
            return IsPaused ? false : Input.GetKeyUp(name);
        }

        public static bool GetKeyUp(KeyCode key)
        {
            return IsPaused ? false : Input.GetKeyUp(key);
        }

        public static bool GetMouseButton(int button)
        {
            return IsPaused ? false : Input.GetMouseButton(button);
        }

        public static bool GetMouseButtonDown(int button)
        {
            return IsPaused ? false : Input.GetMouseButtonDown(button);
        }

        public static bool GetMouseButtonUp(int button)
        {
            return IsPaused ? false : Input.GetMouseButtonUp(button);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
    }
}