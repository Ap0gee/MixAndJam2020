//#define LOG_TRACE_INFO

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameJam.Managers {

    public class SceneStateManager : MonoBehaviour
    {
        public static SceneStateManager instance;

        [SerializeField]
        private string m_nextScene;

        private string m_currentScene;

        private AsyncOperation m_taskResourceUnload;

        private AsyncOperation m_taskSceneLoad;

        [SerializeField]
        private Image m_loadingOverlay;

        private bool m_switching = false;

        private static bool Switching
        {
            get { return instance.m_switching; }
            set { instance.m_switching = value; }
        }

        public static bool IsDoneLoading
        {
            get { return instance.m_taskSceneLoad.isDone; }
        }

        public static bool IsDoneUnloading
        {
            get { return instance.m_taskResourceUnload.isDone; }
        }

        private static void LoadNextScene()
        {
            instance.m_taskSceneLoad = SceneManager.LoadSceneAsync(instance.m_nextScene);
        }

        private static void UnloadUnusedAssets()
        {
            instance.m_taskResourceUnload = Resources.UnloadUnusedAssets();
        }

        private static bool ReadyToSwitch
        {
            get { return instance.m_nextScene != instance.m_currentScene && instance.m_nextScene != null; }
        }

        public static void SwitchScene(string scene)
        {
            if (instance.m_currentScene != scene)
            {
                instance.m_nextScene = scene;
            }   
        }

        public static void ReloadScene()
        { 
            if (instance.m_currentScene != null)
            {
                instance.m_nextScene = instance.m_currentScene;
                instance.m_currentScene += "#";
            }   
        }

        void Awake()
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

        void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        private void Update()
        {
            if (instance)
            {
                if (ReadyToSwitch && !Switching)
                {
                    Switching = true;
                    UnloadUnusedAssets();
                    LoadNextScene();
                }

                if (Switching)
                {
                    if (!instance.m_taskSceneLoad.isDone)
                    {
                        //show loading screen
                    }
                    else
                    {
                        //hide loading screen
                        instance.m_currentScene = instance.m_nextScene;
                        Switching = false;
                    }
                }
            }
        }
    }
}