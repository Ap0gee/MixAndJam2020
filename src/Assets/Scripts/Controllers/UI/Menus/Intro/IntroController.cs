using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;

namespace GameJam
{
    public class IntroController : MonoBehaviour
    {
        [SerializeField]
        private string m_mainScene;

        public void PlayGame()
        {
            SceneStateManager.SwitchScene(m_mainScene);
        }
    }
}