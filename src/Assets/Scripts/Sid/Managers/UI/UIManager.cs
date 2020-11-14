using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJam.Core.State;

namespace GameJam.Managers {

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField]
        private UIController m_ui;

        public static UIController UI
        {
            get { return instance.m_ui; }
        }

        public void Awake()
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

        protected void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
    }
}

