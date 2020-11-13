using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

namespace GameJam.Managers
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager instance;

        [SerializeField]
        private HighlightProfile m_globalHighlightProfile;

        public static HighlightProfile GlobalHighlightProfile
        {
            get { return instance.m_globalHighlightProfile; }
        }

#if UNITY_EDITOR
        void Awake()
        {
            instance = this;
        }
#else
        void Awake()
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
#endif
    }
}