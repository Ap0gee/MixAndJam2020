using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameJam.Core.State;

namespace GameJam.Managers {

    public class GameManager : MonoBehaviour
    {   
        public static GameManager instance;

        private float timeScale = 1.0f;
        private bool isPaused = false;

        [SerializeField]
        private HouseController m_houseController;
        
        public static HouseController House
        {
            get { return instance.m_houseController; }
        }

        [SerializeField]
        private ResourceController m_resourceController;

        public static ResourceController Resources
        {
            get { return instance.m_resourceController; }
        }

        [SerializeField]
        private ControllerPlayer m_player;

        public static ControllerPlayer Player
        {
            get { return instance.m_player; }
        }

        public static bool IsPaused
        {
            get { return instance.isPaused; }
            private set { instance.isPaused = value; }
        }

        public static float TimeScale
        {
            get { return instance.timeScale; }
        }

        public static void PauseGame()
        {
            IsPaused = true;
        }

        public static void UnPauseGame()
        {
            IsPaused = false;
        }

        public static void TogglePauseGame()
        {
            IsPaused = !IsPaused;
        }

        public void Awake()
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

        public void Start()
        {
            
        }

        protected void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                TogglePauseGame();
                InputManager.TogglePauseInput();
            }

            if (House.Destroyed)
            {
                PauseGame();
                InputManager.PauseInput();
                SceneStateManager.SwitchScene("GameOver");
            }
        }
    }
}