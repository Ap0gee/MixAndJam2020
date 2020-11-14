using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameJam
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField]
        private Image m_healthBar;

        [SerializeField]
        private Animation m_skullAnimation;

        [SerializeField]
        private float m_maxHealth = 100f;

        [SerializeField]
        private float m_health;

        public float Health
        {
            get { return m_health; }
            set { if (value < 0) { m_health = 0; } else if (m_health > m_maxHealth) { m_health = m_maxHealth; } else { m_health = value; } }
        }

        private void Awake()
        {
            m_health = m_maxHealth;
        }

        public bool Destroyed
        {
            get { return m_health == 0; }
        }

        private void Update()
        {
            if (m_health <= 25) {
                m_skullAnimation.Play();
            }
            else {
                m_skullAnimation.Stop();
            }

            m_healthBar.fillAmount = 1 / m_maxHealth * m_health;

            Debug.Log(Health);
        }
    }
}