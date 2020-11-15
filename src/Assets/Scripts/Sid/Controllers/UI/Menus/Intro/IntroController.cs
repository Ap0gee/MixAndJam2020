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
        private CanvasGroup m_title;

        [SerializeField]
        private CanvasGroup m_credits;

        [SerializeField]
        private CanvasGroup m_settings;

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

        public void ShowSettings()
        {
            m_title.gameObject.SetActive(false);
            m_settings.gameObject.SetActive(true);
        }

        public void HideSettings()
        {
            m_settings.gameObject.SetActive(false);
            m_title.gameObject.SetActive(true);
        }

        public void ShowCredits()
        {
            m_title.gameObject.SetActive(false);
            m_credits.gameObject.SetActive(true);
        }

        public void HideCredits()
        {
            m_credits.gameObject.SetActive(false);
            m_title.gameObject.SetActive(true);
        }
    }
}