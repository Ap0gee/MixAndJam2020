using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;
using System;
namespace GameJam
{
    public class IntroController : MonoBehaviour
    {
        [SerializeField]
        private string m_mainScene;

        [SerializeField]
        private AudioSource m_audioSource;

        [SerializeField]
        private float m_greetingTime = 1.5f;

        private IEnumerator GoodEveningSir()
        {
            m_audioSource.Play();
            yield return new WaitForSeconds(m_greetingTime);
            SceneStateManager.SwitchScene(m_mainScene);
        }

        public void PlayGame()
        {
            StartCoroutine(GoodEveningSir());
        }
    }
}