//#define LOG_TRACE_INFO

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameJam.Managers {

    public class SceneStateManager : MonoBehaviour
    {
        private static SceneStateManager m_instance;

        public static SceneStateManager Instance
        {
            get {
                if (m_instance == null)
                {
                    //ugh, this is bad
                    m_instance =  GameObject.Find("Managers").GetComponent<SceneStateManager>();
                    return m_instance;
                }
                else
                {
                    return m_instance;
                }
            }
        }

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
            get { return Instance.m_switching; }
            set { Instance.m_switching = value; }
        }

        public static bool IsDoneLoading
        {
            get { return Instance.m_taskSceneLoad.isDone; }
        }

        public static bool IsDoneUnloading
        {
            get { return Instance.m_taskResourceUnload.isDone; }
        }

        private static void LoadNextScene()
        {
            Instance.m_taskSceneLoad = SceneManager.LoadSceneAsync(Instance.m_nextScene);
        }

        private static void UnloadUnusedAssets()
        {
            Instance.m_taskResourceUnload = Resources.UnloadUnusedAssets();
        }

        private static bool ReadyToSwitch
        {
            get { return Instance.m_nextScene != Instance.m_currentScene && Instance.m_nextScene != null; }
        }

        public static void SwitchScene(string scene)
        {
            if (Instance.m_currentScene != scene)
            {
                Instance.m_nextScene = scene;
            }   
        }

        public static void ReloadScene()
        { 
            if (Instance.m_currentScene != null)
            {
                Instance.m_nextScene = Instance.m_currentScene;
                Instance.m_currentScene += "#";
            }   
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (m_instance == null)
            {
                m_instance = this;
            }
            else if (m_instance != this)
            {
                Destroy(this);
            }  
        }

        void OnDestroy()
        {
            if (m_instance != null)
            {
                m_instance = null;
            }
        }

        private void Update()
        {
            if (Instance)
            {
                if (ReadyToSwitch && !Switching)
                {
                    Switching = true;
                    UnloadUnusedAssets();
                    LoadNextScene();
                }

                if (Switching)
                {
                    if (!Instance.m_taskSceneLoad.isDone)
                    {
                        //show loading screen
                    }
                    else
                    {
                        //hide loading screen
                        Instance.m_currentScene = Instance.m_nextScene;
                        Switching = false;
                    }
                }
            }
        }
    }
}