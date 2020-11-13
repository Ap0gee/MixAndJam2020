using UnityEngine;
using GameJam.Managers._Input.States;
using GameJam.Core.State;

namespace GameJam.Managers
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;

        private FiniteStateMachine stateMachine;

        private bool isPaused = false;

        private readonly InputState inputState;

        [SerializeField]
        private ThirdPersonController m_playerController;

        private static ThirdPersonController PlayerController
        {
            get { return instance.m_playerController; }
        } 

        public enum InputState
        {
            Run,
            Pause
        }

        private static FiniteStateMachine StateMachine
        {
            get { return instance.stateMachine; }
            set { instance.stateMachine = value; }
        }

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

        public static void SetPlayerControllerPaused(bool paused)
        {
            PlayerController.controllerPauseState = paused;
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

        private void Start()
        {
            StateMachine = new FiniteStateMachine();

            StateMachine.AddState((int)InputState.Pause, new Pause(StateMachine));
            StateMachine.AddState((int)InputState.Run, new Run(StateMachine));

            State nextState = StateMachine.GetState((int)InputState.Run);
            StateMachine.SetState(nextState);
        }

        private void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

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

        private void Update()
        {
            StateMachine?.Update();
        }
    }
}