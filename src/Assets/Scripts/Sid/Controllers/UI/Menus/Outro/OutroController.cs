using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;

namespace GameJam
{
    public class OutroController : MonoBehaviour
    {
        [SerializeField]
        private string m_introScene;

        [SerializeField]
        private string m_mainScene;

        [SerializeField]
        private AudioSource m_difficultyAudioSource;

        [SerializeField]
        private AudioSource m_laughAudioSource;

        [SerializeField]
        private float m_partingTime = 3f;

        private IEnumerator Muhaha()
        {
            m_difficultyAudioSource.Play();
            yield return new WaitForSeconds(m_partingTime);
            SceneStateManager.SwitchScene(m_mainScene);
        }

        public void PlayAgain()
        {
            m_laughAudioSource.Stop();
            StartCoroutine(Muhaha());
        }

        public void LeaveGame()
        {
            m_laughAudioSource.Stop();
            Application.Quit();
        }

        public void GoToIntro()
        {
            m_laughAudioSource.Stop();
            SceneStateManager.SwitchScene(m_introScene);
        }
    }
}