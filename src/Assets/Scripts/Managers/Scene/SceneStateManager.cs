//#define LOG_TRACE_INFO

using UnityEngine;
using GameJam.Core.State;
using GameJam.Managers._Scene.States;
using UnityEngine.SceneManagement;

namespace GameJam.Managers {

    public class SceneStateManager : MonoBehaviour
    {
        private static SceneStateManager instance;

        private FiniteStateMachine stateMachine;

        private readonly SceneState sceneState;

        [SerializeField]
        private string nextScene;

        private string currentScene;

        private AsyncOperation taskResourceUnload;

        private AsyncOperation taskSceneLoad;

        private static FiniteStateMachine StateMachine
        {
            get { return instance.stateMachine; }
            set { instance.stateMachine = value; }
        }

        private static AsyncOperation TaskResourceUnload
        {
            get { return instance.taskResourceUnload; }
            set { instance.taskResourceUnload = value; }
        }

        private static AsyncOperation TaskSceneLoad
        {
            get { return instance.taskSceneLoad; }
            set { instance.taskSceneLoad = value; }
        }

        private static string NextScene
        {
            get { return instance.nextScene; }
            set { instance.nextScene = value; }
        }

        private static string CurrentScene
        {
            get { return instance.currentScene; }
            set { instance.currentScene = value; }
        }

        public enum SceneState
        {
            Reset,
            Preload,
            Load,
            Unload,
            Postload,
            Ready,
            Run,
            Count
        };

        public struct EventNames
        {
            public static string LoadNextScene
            {
                get { return "SceneStateManager.LoadNextScene"; }
            }
            public static string UnloadUnusedAssets
            {
                get { return "SceneStateManager.UnloadUnusedAssets"; }
            }
            public static string FinalizeSceneSwitch
            {
                get { return "SceneStateManager.FinalizeSceneSwitch"; }
            }
        };

        public static bool IsDoneLoading
        {
            get { return TaskSceneLoad != null ? TaskSceneLoad.isDone : true; }
        }

        public static bool IsDoneUnloading
        {
            get { return TaskSceneLoad != null ? TaskResourceUnload.isDone : true; }
        }

        public static bool IsResetReady
        {
            get { return CurrentScene != NextScene; }
        }

        private void LoadNextScene()
        {
            if (NextScene != null)
            {
                TaskSceneLoad = SceneManager.LoadSceneAsync(NextScene);
            }
        }

        private void UnloadUnusedAssets()
        {
            if (TaskResourceUnload == null)
            {
                TaskResourceUnload = Resources.UnloadUnusedAssets();
            }
        }

        private void FinalizeSceneSwitch()
        {
            CurrentScene = NextScene;
        }

        public static void SwitchScene(string scene)
        {
            if (CurrentScene != scene)
            {
                NextScene = scene;
            }   
        }

        public static void ReloadScene()
        { 
            if (CurrentScene != null)
            {
                NextScene = CurrentScene;
                CurrentScene += "#";
            }   
        }

        private void HookEvents()
        {
            EventManager.StartListening(EventNames.LoadNextScene, LoadNextScene);
            EventManager.StartListening(EventNames.UnloadUnusedAssets, UnloadUnusedAssets);
            EventManager.StartListening(EventNames.FinalizeSceneSwitch, FinalizeSceneSwitch);
        }

        private void UnhookEvents()
        {
            EventManager.StopListening(EventNames.LoadNextScene, LoadNextScene);
            EventManager.StopListening(EventNames.UnloadUnusedAssets, UnloadUnusedAssets);
            EventManager.StopListening(EventNames.FinalizeSceneSwitch, FinalizeSceneSwitch);
        }

        private void Start()
        {
            StateMachine = new FiniteStateMachine();

            StateMachine.AddState((int)SceneState.Reset, new Reset(StateMachine));
            StateMachine.AddState((int)SceneState.Preload, new Preload(StateMachine));
            StateMachine.AddState((int)SceneState.Load, new Load(StateMachine));
            StateMachine.AddState((int)SceneState.Unload, new Unload(StateMachine));
            StateMachine.AddState((int)SceneState.Postload, new PostLoad(StateMachine));
            StateMachine.AddState((int)SceneState.Ready, new Ready(StateMachine));
            StateMachine.AddState((int)SceneState.Run, new Run(StateMachine));

            State nextState = StateMachine.GetState((int)SceneState.Reset);
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

        private void OnEnable()
        {
            HookEvents();
        }

        private void OnDisable()
        {
            UnhookEvents();
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